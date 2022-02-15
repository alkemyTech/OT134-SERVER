using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs;
using OngProject.Core.Models.Paged;
using OngProject.Core.Models.PagedResourceParameters;
using OngProject.Core.Models.Response;
using OngProject.Entities;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    [SwaggerTag("Categories", "Controller to create, read, update and delete categories entities.")]
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

        /// GET: categories
        /// <summary>
        ///    Get categories information.
        /// </summary>
        /// <remarks>
        ///     Get information paged about the categories to the database.
        /// </remarks>
        /// <param name="PageNumber">(optional) Page number, if present it must be a number greather than 0.</param>
        /// <param name="PageSize">(optional) Page Size, number of results per page, if present it must be be a number beetween 1 and 50.</param>
        /// <response code="200">OK. Returns categories information.</response>  
        /// <response code="400">Bad request. Invalid request received.</response>     
        /// <response code="401">Unauthorized. Invalid JWT Token or it wasn't provided.</response>
        /// <response code="404">Not found. Server couldn't find categories.</response> 
        /// <response code="500">Internal Server Error.</response> 
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Result<PagedResponse<CategoryDtoForDisplay>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllCategories([FromQuery] PaginationParams pagingParams)
        {            
            var result = await _categoryService.GetAll(pagingParams);
            
            return StatusCode(result.StatusCode, result);
        }

        // GET: categories/5
        /// <summary>
        ///    Get a Category information.
        /// </summary>
        /// <remarks>
        ///     Get information about the category with the id provided.
        /// </remarks>
        ///  <param name="id">Category id that will be searched.</param>
        /// <response code="200">OK. Returns the category information.</response>  
        /// <response code="400">Bad request. Invalid request received.</response> 
        /// <response code="401">Unauthorized. Invalid JWT Token or it wasn't provided.</response>
        /// <response code="404">Not found. Server couldn't find the category with the id provided.</response> 
        /// <response code="500">Internal Server Error.</response>
        [HttpGet("{id}")]
        [Authorize(Roles = "Administrator")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Result<CategoryDtoForDisplay>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCategoryById(int id)
        {            
            var result = await _categoryService.GetById(id);

            return StatusCode(result.StatusCode, result);           
        }

        /// POST: categories
        /// <summary>
        ///     Creates a new Category.
        /// </summary>
        /// <remarks>
        ///     Adds a new Category to the database.
        /// </remarks>
        /// <param name="categoryDTO">New Category data transfer object.</param>
        /// <response code="200">OK. Returns a result object along with the new category information.</response>        
        /// <response code="400">BadRequest. Category could not be created.</response>    
        /// <response code="401">Unauthorized. Invalid JWT Token or it wasn't provided.</response>    
        /// <response code="500">Internal Server Error.</response>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Result<Category>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]        
        [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]        
        public async Task<IActionResult> Insert([FromForm] CategoryDTOForRegister categoryDTO)
        {
            var response = await _categoryService.Insert(categoryDTO);
            
            return StatusCode(response.StatusCode, response);
        }

        /// PUT: categories
        /// <summary>
        ///     Update a Category.
        /// </summary>
        /// <remarks>
        ///     Update a Category to the database.
        /// </remarks>
        /// <param name="id">Category id that will be removed.</param>
        /// <param name="categoryDTO">Category data transfer object.</param>
        /// <response code="200">OK. Returns a result object if the category was successfully updated.</response>        
        /// <response code="400">BadRequest. Category could not be updated.</response>    
        /// <response code="401">Unauthorized. Invalid JWT Token or it wasn't provided.</response>
        /// <response code="404">Not found. Server couldn't find the category with the id provided.</response> 
        /// <response code="500">Internal Server Error.</response>
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]        
        [Produces("application/json")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, [FromForm] CategoryDTOForUpload dto)
        {
            var result = await _categoryService.Update(id, dto);

            return StatusCode(result.StatusCode, result);
        }

        /// DELETE: categories/1
        /// <summary>
        ///     Delete a Category.
        /// </summary>
        /// <remarks>
        ///     Delete a Category to the database.
        /// </remarks>
        /// <param name="id">Category id that will be removed.</param>
        /// <response code="200">OK. Returns a result object if the category was successfully removed.</response>        
        /// <response code="400">BadRequest. Category could not be removed.</response>    
        /// <response code="401">Unauthorized. Invalid JWT Token or it wasn't provided.</response>
        /// <response code="404">Not found. Server couldn't find the category with the id provided.</response> 
        /// <response code="500">Internal Server Error.</response>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Result<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Result<List<string>>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _categoryService.Delete(id);
            
            return StatusCode(result.StatusCode, result);
        }

    }

}
