using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs;
using OngProject.Core.Models.Response;
using OngProject.Entities;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    [SwaggerTag("User", "Controller to create, read, update and delete users entities.")]
    [Route("users")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// GET: users
        /// <summary>
        ///     Get all users information.
        /// </summary>
        /// <remarks>
        ///     Get the information about all the users stored in the database.
        /// </remarks>
        /// <response code="200">OK. Returns user information.</response>  
        /// <response code="400">Bad request. Invalid request received.</response>    
        /// <response code="401">Unauthorized. Invalid JWT Token or it wasn't provided.</response>     
        /// <response code="404">Not Found. Server couldn't find any user.</response> 
        /// <response code="500">Internal Server Error.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            
            var result = await _userService.GetAll();

            return StatusCode(result.StatusCode, result);            
        }
        

        /// Post: user/5
        /// <summary>
        ///     Update a user information.
        /// </summary>
        /// <remarks>
        ///     Update a user information with the values provided.
        /// </remarks>
        /// <param name="id">User ID that will be updated.</param>
        /// <param name="UserUpdateDto">Dto that allow to update the user</param>
        /// <response code="200">OK. Returns a result object.</response>  
        /// <response code="400">Bad request. User couldn't be updated.</response>    
        /// <response code="401">Unauthorized. Invalid JWT Token or it wasn't provided.</response>     
        /// <response code="404">Not Found. Server couldn't find any user with the ID provided.</response> 
        /// <response code="500">Internal Server Error.</response>
        [HttpPut("id")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]        
        public async Task<IActionResult> Put(int id, [FromForm] UserUpdateDto user)
        {
            //var claimId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            var response = await _userService.Update(id, user ); //Int32.Parse(claimId.Value)
            return StatusCode(response.StatusCode, response);
        }

        /// Delete: user/5
        /// <summary>
        ///     Delete a user.
        /// </summary>
        /// <remarks>
        ///     Delete the user from the database.
        /// </remarks>
        /// <param name="id">User ID that will be deleted.</param>
        /// <response code="200">OK. Returns a result object.</response>  
        /// <response code="400">Bad request. User couldn't be updated..</response>    
        /// <response code="401">Unauthorized. Invalid JWT Token or it wasn't provided.</response>     
        /// <response code="404">Not Found. Server couldn't find any user with the ID provided.</response> 
        /// <response code="500">Internal Server Error.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int id)
        {            
            var result = await this._userService.Delete(id);
                     
            return StatusCode(result.StatusCode, result);
        }
    }
}

