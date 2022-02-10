using Microsoft.AspNetCore.Http;
using OngProject.Core.Helper;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs;
using OngProject.Core.Models.Paged;
using OngProject.Core.Models.PagedResourceParameters;
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
        private readonly IHttpContextAccessor _httpContext;

        public CategoryService(IUnitOfWork unitOfWork, IImageService imageService, IEntityMapper entityMapper, IHttpContextAccessor httpContext)
        {
            _unitOfWork = unitOfWork;
            _imageService = imageService;
            _entityMapper = entityMapper;
            _httpContext = httpContext;
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

        public async Task<Result> GetAll(PaginationParams paginationParams)
        {
            try
            {
                var categories = await _unitOfWork.CategoryRepository.FindAllAsync();

                if(categories.Count == 0)
                {
                    return Result.FailureResult("No existen categorias");
                }

                var categoriesDTOForDisplay = categories
                    .Select(category => _entityMapper.CategoryToCategoryDtoForDisplay(category));

                var paged =  PagedList<CategoryDtoForDisplay>.Create(categoriesDTOForDisplay.ToList(),
                                                                paginationParams.PageNumber,
                                                                paginationParams.PageSize);

                var url = $"{this._httpContext.HttpContext.Request.Scheme}://{this._httpContext.HttpContext.Request.Host}{this._httpContext.HttpContext.Request.Path}";
                var pagedResponse = new PagedResponse<CategoryDtoForDisplay>(paged, url);
                
                return Result<PagedResponse<CategoryDtoForDisplay>>.SuccessResult(pagedResponse);
            }
            catch(Exception e)
            {
                return Result.ErrorResult(new List<string> { e.Message });
            }
        }

        public async Task<Result> GetById(int id)
        {
            try
            {
                var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
                if (category != null)
                {
                    var categoryDto = _entityMapper.CategoryToCategoryDtoForDisplay(category);
                    return Result<CategoryDtoForDisplay>.SuccessResult(categoryDto);
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
