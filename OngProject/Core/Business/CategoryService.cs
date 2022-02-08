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

        public async Task<IEnumerable<CategoryDTO>> GetAll()
        {
            var categories = await _unitOfWork.CategoryRepository.FindAllAsync();

            var categoriesDTO = categories              
                .Select(category => _entityMapper.CategoryToCategoryDTO(category));

            return categoriesDTO;
        }

        public Category GetById()
        {
            throw new System.NotImplementedException();
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
