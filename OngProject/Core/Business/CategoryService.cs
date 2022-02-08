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
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntityMapper _entityMapper;
        private readonly IImageService _imageService;

        public CategoryService(IUnitOfWork unitOfWork, IImageService imageService)
        {
            _unitOfWork = unitOfWork;
            _imageService = imageService;
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

        public Category GetById()
        {
            throw new System.NotImplementedException();
        }

        public async Task<Result> Insert(CategoryDTO categoryDTO)
        {
            string imageName = String.Empty,
                image = categoryDTO.Name != null ? categoryDTO.Image.Name : String.Empty;
            try
            {

                if (image != String.Empty)
                {
                    imageName = await _imageService.UploadFile($"{Guid.NewGuid()}_{categoryDTO.Image.FileName}", categoryDTO.Image);
                }

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
