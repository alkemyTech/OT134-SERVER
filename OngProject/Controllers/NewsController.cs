using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs;
using OngProject.Core.Models.PagedResourceParameters;
using System;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OngProject.Controllers
{
    [Route("news")]
    [ApiController]
    [Authorize]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;
        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        /// <summary>
        ///     Get all News information Add In To The DataBase.
        /// </summary>
        /// <example>
        ///This shows how to increment an integer.
        ///<code>
        ///  var index = 5;
        ///  index++;
        ///</code>
        ///</example>
        ///<param name="pagingParams">Pagination parameters to display the testimonials by pages.</param>
        /// <response code="500">Internal Server Error.</response>              
        /// <response code="200">OK. Return an object Result that includes the News.</response>        
        /// <response code="400">BadRequest. Return an object Result with an error message that indicate the cause of the problem.</response>  
        /// <response code="401">Authorization Required. Returns a Result object with a message indicating that the cause of the problem is that the user did not register and/or log in to the system.</response>  
        /// <response code="404">Not found.The server does not contain news.</response> 
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllNews([FromQuery] PaginationParams pagingParams)
        {
            var result = await _newsService.GetAll(pagingParams);

            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        ///     Get a novelty based on its id.
        /// </summary>
        /// <remarks>
        ///     it should be noted that the id field is mandatory.
        /// </remarks>
        ///<param name="id">id of the novelty you want to find</param>
        ///<example>
        ///this shows how you should send the id to look for a new
        ///<code>
        ///   https://localhost:44360/api/New/Get/1
        ///</code>
        ///</example>
        /// <response code="500">Internal Server Error.</response>              
        /// <response code="200">OK. Return an object Result that includes the News.</response>        
        /// <response code="400">BadRequest. Return an object Result with an error message that indicate the cause of the problem.</response>  
        /// <response code="401">Authorization Required. Returns a Result object with a message indicating that the cause of the problem is that the user did not register and/or log in to the system.</response>  
        /// <response code="404">Not found. Server couldn't find the new with the id provided.</response> 
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result = await _newsService.GetById(id);
                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        ///     register a new novelty in the database.
        /// </summary>
        /// <remarks>
        ///     only an admin user can access this functionality.
        ///     it should be noted that all its fields are mandatory.
        /// </remarks>
        ///<param name="newDtoForUpload">dto that will allow me to add a new novelty</param>
        /// <response code="500">Internal Server Error.</response>              
        /// <response code="200">OK. Return an object Result that includes the News.</response>        
        /// <response code="400">BadRequest. Return an object Result with an error message that indicate the cause of the problem.</response>  
        /// <response code="401">Authorization Required. Returns a Result object with a message indicating that the cause of the problem is that the user did not register and/or log in to the system.</response>  
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Post([FromForm] NewDtoForUpload newDtoForUpload)
        {
            try
            {
                var result = await _newsService.Insert(newDtoForUpload);
                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        ///     update a novelty in the database.
        /// </summary>
        /// <remarks>
        ///     only an admin user can access this functionality.
        ///     only the id will be required
        /// </remarks>
        ///<param name="newsDTO">dto that will allow me to update a desired novelty</param>
        ///<param name="id">the id refers to the identifier of the novelty that we want to update</param>
        /// <response code="500">Internal Server Error.</response>              
        /// <response code="200">OK. Return an object Result that includes the News.</response>        
        /// <response code="400">BadRequest. Return an object Result with an error message that indicate the cause of the problem.</response>  
        /// <response code="401">Authorization Required. Returns a Result object with a message indicating that the cause of the problem is that the user did not register and/or log in to the system.</response>  
        /// <response code="404">Not found. Server couldn't find the New with the id provided.</response> 
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Put(int id, [FromForm] NewDtoForUpload newsDTO)
        {
            var result = await _newsService.Update(id, newsDTO);
            if (result.Success)
            {
                return Ok(result);
            }
            return StatusCode(result.isError() ? 500 : 400, result);
        }
        /// <summary>
        ///     update a novelty in the database.
        /// </summary>
        /// <remarks>
        ///     only an admin user can access this functionality.
        ///     only the id will be required
        /// </remarks>
        ///<param name="id">the id allows me to search for the news you want to unsubscribe from and if it is active it will be unsubscribed automatically</param>
        /// <response code="500">Internal Server Error.</response>              
        /// <response code="200">OK. Return an object Result that includes the News.</response>        
        /// <response code="400">BadRequest. Return an object Result with an error message that indicate the cause of the problem.</response>  
        /// <response code="401">Authorization Required. Returns a Result object with a message indicating that the cause of the problem is that the user did not register and/or log in to the system.</response>  
        /// <response code="404">Not found. Server couldn't find the New with the id provided.</response> 
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await this._newsService.Delete(id);
                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}