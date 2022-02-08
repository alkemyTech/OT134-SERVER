using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs;
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
        private readonly IImageService _imageService;
        public CategoryController(ICategoryService categoryService, IImageService imageService)
        {
            _categoryService = categoryService;
            _imageService = imageService;
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
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Insert([FromForm] CategoryDTOForRegister categoryDTO)
        {

            var response = await _categoryService.Insert(categoryDTO);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpPut("{id}")]
        public void UpdateCategory()
        {
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int id)
        {

            var result = await _categoryService.Delete(id);
            if (result.Success)
            {
                return Ok(result);
            }


            return BadRequest(result);
        }

    }

}
