using Microsoft.AspNetCore.Http;
using OngProject.Core.Helper.formFile;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs;
using OngProject.Core.Models.Response;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Business
{
    public class SlideService : ISlideSerivice
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntityMapper _mapper;
        private readonly IImageService _imageService;

        public SlideService(IUnitOfWork unitOfWork, IEntityMapper mapper, IImageService imageService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _imageService = imageService;
        }

        public async Task<Result> Delete(int id)
        {
            try
            {
                Slides slide = await _unitOfWork.SlideRepository.GetByIdAsync(id);

                if (slide != null && !slide.SoftDelete)
                {
                    slide.SoftDelete = true;
                    slide.LastModified = DateTime.Now;
                    _unitOfWork.SaveChanges();

                    return Result<string>.SuccessResult("Slide borrado correctamente.");
                }
                else
                {
                    if (slide == null)
                        return Result.FailureResult("No se encontro ningun Slide con Id ingresado");
                    else
                        return Result.FailureResult("Slide con Id ingresado ha sido eliminado previamente");
                }
            }
            catch (Exception ex)
            {
                return Result.FailureResult(ex.Message);
            }
        }

        public async Task<ICollection<SlideDtoForDisplay>> GetAll()
        {
            var response = await _unitOfWork.SlideRepository.FindByConditionAsync(x => x.SoftDelete == false);
            if (response.Count == 0)
            {
                return null;
            }
            else 
            {
                List<SlideDtoForDisplay> dto = new ();
                foreach (var item in response)
                {
                    dto.Add(_mapper.SlideToSlideDtoForDisplay(item));
                }
                return dto;
            }
        }

        public async Task<Result> GetById(int id)
        {
            try
            {
                Slides slide = await _unitOfWork.SlideRepository.GetByIdAsync(id);

                if (slide == null)
                    return Result.FailureResult("No se encontro ningun Slide con Id ingresado");
                else if (slide.SoftDelete != false)
                    return Result.FailureResult("Slide con Id ingresado ha sido eliminado previamente");

                var slideDto = _mapper.SlideToSlideById(slide);
                return Result<SlideDtoById>.SuccessResult(slideDto);
            }
            catch(Exception ex)
            {
                return Result.ErrorResult(new List<string>{ex.Message});
            }
        }

        public async Task<Result> Insert(SlideDtoForUpload slideDto)
        {
            try
            {
                if (string.IsNullOrEmpty(slideDto.ImageUrl))
                    return Result.FailureResult("Se debe ingresar Imagen");
                if (string.IsNullOrEmpty(slideDto.Text))
                    return Result.FailureResult("Se debe ingresar Texto");
                if (slideDto.OrganizationId < 1)
                    return Result.FailureResult("Se debe ingresar Id para Organizacion entero y mayor a cero");
                if (slideDto.Order < 0)
                    return Result.FailureResult("El numero de Orden debe ser mayor a cero");

                if (slideDto.Order == 0)
                {
                    var slidesList = await _unitOfWork.SlideRepository.FindByConditionAsync(
                        x => x.OrganizationId == slideDto.OrganizationId && x.Order == slideDto.Order);

                    slidesList = slidesList.OrderBy(x => x.Order).ToList();

                    slideDto.Order = slidesList.LastOrDefault().Order + 1;
                }
                else
                {
                    var slidesList = await _unitOfWork.SlideRepository.FindByConditionAsync(
                        x => x.OrganizationId == slideDto.OrganizationId && x.Order == slideDto.Order);

                    if (slidesList.Count != 0)
                        return Result.FailureResult("Numero de Orden ya Ingresado anteriormente en Organizacion Ingresada");
                }

                var slide = _mapper.SlideDtoForUploadToSlide(slideDto);
                slide.ImageUrl = await UploadEncodedImageToBucketAsync(slideDto.ImageUrl);
                slide.LastModified = DateTime.Now;

                await _unitOfWork.SlideRepository.Create(slide);
                _unitOfWork.SaveChanges();

                var newSlideDto = _mapper.SlideToSlideDtoForDisplay(slide);

                return Result<SlideDtoForDisplay>.SuccessResult(newSlideDto);
            }
            catch (Exception ex)
            {
                return Result.FailureResult(ex.Message);
            }
        }

        public async Task<Result> Update(int id, SlideDtoForUpdate dto)
        {
            try
            {
                var slide = await _unitOfWork.SlideRepository.GetByIdAsync(id);

                if (slide != null)
                {
                    slide.Text = dto.Text;
                    slide.Order = dto.Order;
                    slide.LastModified = DateTime.Now;

                    if (dto.ImageUrl == null && slide.ImageUrl != null || dto.ImageUrl != null && slide.ImageUrl != null)
                    {
                        await _imageService.AwsDeleteFile(slide.ImageUrl[(slide.ImageUrl.LastIndexOf("/") + 1)..]);
                        slide.ImageUrl = null;
                    }
                    if (dto.ImageUrl != null)
                    {
                        slide.ImageUrl = await UploadEncodedImageToBucketAsync(dto.ImageUrl);
                    }

                    await _unitOfWork.SaveChangesAsync();

                    var slideDto = _mapper.SlideToSlideDtoForDisplay(slide);

                    return Result<SlideDtoForDisplay>.SuccessResult(slideDto);
                }

                return Result.FailureResult("Id de Slide inexistente.");
            }
            catch (Exception ex)
            {
                return Result.ErrorResult(new List<string> { ex.Message });
            }
        }

        public async Task<ICollection<SlideDtoForDisplay>> GetAllByOrganization(int idOrganization)
        {
            var response = await _unitOfWork.SlideRepository.FindByConditionAsync(x => x.OrganizationId == idOrganization);
            if (response.Count == 0)
            {
                return null;
            }
            else
            {
                List<SlideDtoForDisplay> dto = new();
                foreach (var item in response)
                {
                    dto.Add(_mapper.SlideToSlideDtoForDisplay(item));
                }
                return dto;
            }
        }

        private async Task<string> UploadEncodedImageToBucketAsync(string rawBase64File)
        {
            string newName = $"{Guid.NewGuid()}_user";

            int indexOfSemiColon = rawBase64File.IndexOf(";", StringComparison.OrdinalIgnoreCase);
            string dataLabel = rawBase64File.Substring(0, indexOfSemiColon);
            string contentType = dataLabel.Split(':').Last();
            var startIndex = rawBase64File.IndexOf("base64,", StringComparison.OrdinalIgnoreCase) + 7;
            var fileContents = rawBase64File.Substring(startIndex);

            var formFileData = new FormFileData()
            {
                FileName = newName,
                ContentType = contentType,
                Name = newName
            };
            byte[] imageBinaryFile = Convert.FromBase64String(fileContents);
            IFormFile newFile = ConvertFile.BinaryToFormFile(imageBinaryFile, formFileData);
            return await _imageService.UploadFile(newFile.FileName, newFile);
        }
    }
}
