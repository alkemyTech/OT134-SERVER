using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
using OngProject.Core.Models.DTOs;
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

        public async Task<CategoryDTO> GetAll()
        {
            var response = await _unitOfWork.CategoryRepository.FindAllAsync();

            return _entityMapper.CategoryToCategoryDTO(response.FirstOrDefault());
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
