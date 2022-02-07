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
        private readonly EntityMapper _entityMapper;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _entityMapper = new EntityMapper();
        }
        public void Delete(Category category)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<CategoryDTO>> GetAll()
        {
            var categories = await _unitOfWork.CategoryRepository.FindAllAsync();

            var categoriesDTO = categories
                .Select(category => _entityMapper.CategoryToCategoryDTO(category));

            return categoriesDTO;
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
                else
                {
                    return Result.FailureResult("Error 404");
                }
                
                

            }
            catch (Exception)
            {

                return Result.FailureResult("Ocurrio un problema al buscar la categoria.");
            }
            
            
        }

        public void Insert(Category category)
        {
            throw new System.NotImplementedException();
        }

        public void Update(Category category)
        {
            throw new System.NotImplementedException();
        }
    }
}
