using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs;
using OngProject.Core.Models.PagedResourceParameters;
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
        public async Task<IActionResult> GetAllCategories([FromQuery] PaginationParams pagingParams)
        {            
            var result = await _categoryService.GetAll(pagingParams);
            
            return StatusCode(result.StatusCode, result);
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
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Put(int id, [FromForm] CategoryDTOForUpload dto)
        {
            var result = await _categoryService.Update(id, dto);
            
            return StatusCode(result.StatusCode, result);
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
