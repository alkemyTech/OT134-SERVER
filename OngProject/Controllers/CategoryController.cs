using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.Response;
using System;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    [Route("categories")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var categoriesDto = await _categoryService.GetAll();
                if (categoriesDto != null)
                    return Ok(categoriesDto);
                else
                    return NotFound("Categorias vacias");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
        
        [HttpGet("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            
                var result = await _categoryService.GetById(id);
                if (result.Success)
                {
                    return Ok(result);
                }

                return StatusCode(404);
           
        }

        [HttpPost]
        public void AddCategory()
        {
        }
        [HttpPut("{id}")]
        public void UpdateCategory()
        {
        }
        [HttpDelete("{id}")]
        public void DeleteCategory(int id)
        {
        }
    }
}
