using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs;
using OngProject.Core.Models.Response;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Core.Business
{
    public class NewsService : INewsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntityMapper _mapper;
        private readonly IImageService _imageService;

        public NewsService(IUnitOfWork unitOfWork, IEntityMapper mapper, IImageService imageService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _imageService = imageService;
        }

        public async Task<ICollection<NewDtoForDisplay>> GetAll()
        {
            var response = await _unitOfWork.NewsRepository.FindByConditionAsync(x => x.SoftDelete == false);
            if (response.Count == 0)
                return null;
            else
            {
                List<NewDtoForDisplay> listOfnewDtoForDisplays = new();
                foreach (var item in response)
                {
                    listOfnewDtoForDisplays.Add(_mapper.NewtoNewDtoForDisplay(item));
                }
                return listOfnewDtoForDisplays;
            }
        }
        public async Task<Result> GetById(int id)
        {
            try
            {
                var newEntity = await this._unitOfWork.NewsRepository.GetByIdAsync(id);
                if (newEntity != null)
                {
                    if (newEntity.SoftDelete)
                    {
                        return Result.FailureResult($"id({newEntity.Id}) se encuentra eliminada del sistema.");
                    }
                    var newToNewDtoForDisplay = _mapper.NewtoNewDtoForDisplay(newEntity);
                    return Result<NewDtoForDisplay>.SuccessResult(newToNewDtoForDisplay);
                }
                return Result.FailureResult("id de noticia inexistente.");
            }
            catch (Exception e)
            {
                return Result.ErrorResult(new List<string> { e.Message });
            }
        }
        public async Task<Result> Insert(NewDtoForUpload newDtoForUpload)
        {
            try
            {
                var newEntity = _mapper.NewDtoForUploadtoNew(newDtoForUpload);

                var ValidationName = await _unitOfWork.NewsRepository.FindByConditionAsync(x => x.Name == newDtoForUpload.Name);
                var ValidationContent = await _unitOfWork.NewsRepository.FindByConditionAsync(x => x.Content == newDtoForUpload.Content);
                var ValidationCategoryId = await _unitOfWork.CategoryRepository.FindByConditionAsync(x => x.Id == newDtoForUpload.Category);
                if (ValidationName.Count > 0)
                    throw new Exception("Una noticia con ese nombre ya existe en el sistema, intente uno diferente al ingresado.");
                if (ValidationContent.Count > 0)
                    throw new Exception("Una noticia con ese contenido ya existe en el sistema, intente uno diferente al ingresado.");
                if (ValidationCategoryId.Count == 0)
                    throw new Exception("No existe una categoria con ese Id en el sistema, intente uno diferente al ingresado.");
                else
                {
                    var imageUploadUrl = await _imageService.UploadFile(newDtoForUpload.Image.FileName, newDtoForUpload.Image);
                    newEntity.Image = imageUploadUrl;
                    newEntity.LastModified = DateTime.Today;

                    await _unitOfWork.NewsRepository.Create(newEntity);
                    await _unitOfWork.SaveChangesAsync();

                    var newtoNewDtoForDisplay = _mapper.NewtoNewDtoForDisplay(newEntity);

                    return Result<NewDtoForDisplay>.SuccessResult(newtoNewDtoForDisplay);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<Result> Update(int id, NewDtoForUpload newsDTO)
        {
            try
            {
                var news = await _unitOfWork.NewsRepository.GetByIdAsync(id);

                if (news != null)
                {
                    await _imageService.AwsDeleteFile(news.Image[(news.Image.LastIndexOf("/") + 1)..]);

                    var imageUrl = await _imageService.UploadFile($"{Guid.NewGuid()}_{newsDTO.Image.FileName}", newsDTO.Image);
                                       
                    news.Name = newsDTO.Name;
                    news.Content = newsDTO.Content;
                    news.Image = imageUrl;
                    news.CategoryId = newsDTO.Category;
                    news.LastModified = DateTime.Now;

                    await _unitOfWork.SaveChangesAsync();

                    var newsDisplay = _mapper.NewtoNewDtoForDisplay(news);

                    return Result<NewDtoForDisplay>.SuccessResult(newsDisplay);
                }
                return Result.FailureResult("Id de noticia inexistente.");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<Result> Delete(int id)
        {
            try
            {
                var newEntity = await this._unitOfWork.NewsRepository.GetByIdAsync(id);
                if (newEntity != null)
                {
                    if (newEntity.SoftDelete)
                    {
                        return Result.FailureResult($"id({newEntity.Id}) ya esta eliminado del sistema.");
                    }
                    newEntity.SoftDelete = true;
                    newEntity.LastModified = DateTime.Today;
                    await this._unitOfWork.SaveChangesAsync();

                    return Result<string>.SuccessResult($"Noticia:({newEntity.Id}) ha sido eliminada exitosamente.");
                }
                return Result.FailureResult("id de noticia inexistente.");
            }
            catch (Exception e)
            {
                return Result.ErrorResult(new List<string> { e.Message });
            }
        }
    }
}