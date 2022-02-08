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
                List<NewDtoForDisplay> newDtoForDisplaysList = new();
                foreach (var item in response)
                {
                    newDtoForDisplaysList.Add(_mapper.NewtoNewDtoForDisplay(item));
                }
                return newDtoForDisplaysList;
            }
        }
        public New GetById()
        {
            throw new NotImplementedException();
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
        public async Task<Result> Update(New news)
        {
            throw new NotImplementedException();
        }
        public async Task<Result> Delete(New news)
        {
            throw new NotImplementedException();
        }
    }
}