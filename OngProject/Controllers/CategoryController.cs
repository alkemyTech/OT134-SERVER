using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;

namespace OngProject.Controllers
{
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _activitiesService;
        public CategoryController(ICategoryService categoryService)
        {
            _activitiesService = categoryService;
        }
        [HttpGet]
        public IActionResult GetAllCategories()
        {
            return Ok();
        }
        [HttpGet("{id}")]
        public IActionResult GetCategoryById(int id)
        {
            return Ok();
        }

        [HttpPost]
        public void AddCategory()
        {
        }
        [HttpPut("{id}")]
        public void UpdateCategory()
        {
        }
        [HttpPut("{id}")]
        public void DeleteCategory(int id)
        {
        }
    }
}
