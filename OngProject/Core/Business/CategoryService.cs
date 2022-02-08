using Microsoft.AspNetCore.Authorization;
using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
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
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntityMapper _entityMapper;
        private readonly IImageService _imageService;

        public CategoryService(IUnitOfWork unitOfWork, IImageService imageService, IEntityMapper entityMapper)
        {
            _unitOfWork = unitOfWork;
            _imageService = imageService;
            _entityMapper = entityMapper;
        }
        public async Task<Result> Delete(int id)
        {
            try
            {
                var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
                if (category != null)
                {
                    if (category.SoftDelete)
                    {
                        return Result.FailureResult("La categoria seleccionada ya fue eliminada");
                    }
                    category.SoftDelete = true;
                    category.LastModified = DateTime.Now;
                    await _unitOfWork.SaveChangesAsync();

                    return Result<Category>.SuccessResult(category);
                }

                return Result.FailureResult("La categoria no existe.");

            }
            catch (Exception e)
            {
                return Result.FailureResult("Error al eliminar la categoria: " + e.Message);
            }
        }

        public async Task<IEnumerable<CategoryDtoForDisplay>> GetAll()
        {
            var categories = await _unitOfWork.CategoryRepository.FindAllAsync();

            var categoriesDTOForDisplay = categories              
                .Select(category => _entityMapper.CategoryToCategoryDtoForDisplay(category));

            return categoriesDTOForDisplay;
        }

        public async Task<Result> GetById(int id)
        {
            try
            {
                var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
                if (category != null)
                {
                    var categoryDto = _entityMapper.CategoryToCategoryDTO(category);
                    return Result<CategoryDTO>.SuccessResult(categoryDto);
                }
                return Result.FailureResult("La categoria no existe.");

            }
            catch (Exception)
            {

                return Result.FailureResult("Ocurrio un problema al buscar la categoria.");
            }
            
            
        }

        public async Task<Result> Insert(CategoryDTOForRegister categoryDTO)
        {
            string imageName = String.Empty,
                image = categoryDTO.Name != null ? categoryDTO.Image.Name : String.Empty;
            try
            {

                if (image != String.Empty)
                    imageName = await _imageService.UploadFile($"{Guid.NewGuid()}_{categoryDTO.Image.FileName}", categoryDTO.Image);

                //Usar el mapper con el DTO
                //Implementar validaciones

                var newCategory = new Category()
                {
                    Description = categoryDTO.Description,
                    Image = imageName,
                    LastModified = DateTime.Now,
                    Name = categoryDTO.Name,
                    SoftDelete = false
                };

                await _unitOfWork.CategoryRepository.Create(newCategory);
                await _unitOfWork.SaveChangesAsync();

                return Result<Category>.SuccessResult(newCategory);

            }
            catch (Exception e)
            {

                return Result.FailureResult("Ocurrio un problema al crear una nueva categoria: " + e.Message);
            }
        }

        public void Update(Category category)
        {
            throw new System.NotImplementedException();
        }
    }
}
