using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using OngProject.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using OngProject.Core.Models.DTOs;
using OngProject.Core.Models.PagedResourceParameters;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Http;

namespace OngProject.Controllers
{
    [SwaggerTag("Members", "Controller to create, read, update and delete members entities.")]
    [Route("members")]
    [ApiController]
    [Authorize]
    public class MembersController : ControllerBase
    {
        private readonly IMemberService _membersService;

        public MembersController(IMemberService memberService)
        {
            _membersService = memberService;
        }

        /// GET: members
        /// <summary>
        ///    Get all Members information.
        /// </summary>
        /// <remarks>
        ///     Gets information about all the current members stored in the database.
        /// </remarks>
        /// <response code="200">OK. Returns members information.</response>  
        /// <response code="401">Unauthorized. Invalid JWT Token or it wasn't provided.</response>     
        /// <response code="404">Not Found. Server couldn't find any members information.</response> 
        /// <response code="500">Internal Server Error.</response>  
        [HttpGet]
        public async Task<IActionResult> GetAllMembers([FromQuery] PaginationParams pagingParams)
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            var result = await _membersService.GetAll(pagingParams);

            return StatusCode(result.StatusCode, result);
        }

        /// GET: members/5
        /// <summary>
        ///    Gets a Member information.
        /// </summary>
        /// <remarks>
        ///     Gets information about the member with the id provided.
        /// </remarks>
        ///  <param name="id">Member id that will be searched.</param>
        /// <response code="200">OK. Returns the member information.</response>  
        /// <response code="401">Unauthorized. Invalid JWT Token or it wasn't provided.</response>     
        /// <response code="404">Not Found. Server couldn't find the member with the id provided.</response> 
        /// <response code="500">Internal Server Error.</response>  
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get(int id)
        {
            return Ok();
        }

        /// POST: members
        /// <summary>
        ///     Creates a new Member.
        /// </summary>
        /// <remarks>
        ///     Adds a new member to the database.
        /// </remarks>
        /// <param name="memberDTO">New Member object.</param>
        /// <response code="200">OK. Returns a result object along with the new member information.</response>        
        /// <response code="400">BadRequest. Member could not be created.</response>    
        /// <response code="401">Unauthorized. Invalid JWT Token or it wasn't provided.</response>    
        /// <response code="500">Internal Server Error.</response>  
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromForm] MemberDTORegister memberDTO)
        {
            try
            {
                var result = await _membersService.Insert(memberDTO);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// PUT: members/5
        /// <summary>
        ///     Updates a Member information.
        /// </summary>
        /// <remarks>
        ///     Updates a member information with the values provided.
        /// </remarks>
        /// <param name="id">Member id that will be updated.</param>
        /// <param name="memberDTO">Current Member information.</param>
        /// <response code="200">OK. Returns a result object along with the new member information.</response>    
        /// <response code="401">Unauthorized. Invalid JWT Token or it wasn't provided.</response>    
        /// <response code="404">Not Found. Server couldn't find the member with the id provided.</response> 
        /// <response code="500">Internal Server Error.</response> 
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public void Put(int id, [FromBody] string memberDTO)
        {
        }

        /// DELETE: members/5
        /// <summary>
        ///     Deletes a Member.
        /// </summary>
        /// <remarks>
        ///     Deletes a member from the database.
        /// </remarks>
        /// <param name="id">Member id that will be deleted.</param>
        /// <response code="200">OK. Returns a success message.</response>    
        /// <response code="401">Unauthorized. Invalid JWT Token or it wasn't provided.</response>    
        /// <response code="404">Not Found. Server couldn't find the member with id provided.</response> 
        /// <response code="500">Internal Server Error.</response>  
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _membersService.Delete(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return StatusCode(result.isError() ? 500 : 400, result);
        }
    }
}