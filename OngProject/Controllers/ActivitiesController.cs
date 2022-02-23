using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs;
using OngProject.Core.Models.Paged;
using OngProject.Core.Models.Response;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    [SwaggerTag("Actividades", "Web API para creacion, modificacion, detalles y borrado de Actividades")]
    [Route("activities")]
    [ApiController]
    [Authorize(Roles = "Administrator")]
    public class ActivitiesController : ControllerBase
    {
        private readonly IActivitiesService _activitiesService;
        public ActivitiesController(IActivitiesService activitiesService)
        {
            _activitiesService = activitiesService;
        }

        /*[HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }*/

        /*// GET api/<ActivitiesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok();
        }*/

        /// POST: Activities
        /// <summary>
        ///     Creates a new Activity.
        /// </summary>
        /// <remarks>
        ///     Adds a new Activity to the database.
        /// </remarks>
        /// <param name="dto">New Activity data transfer object.</param>
        /// <response code="200">OK. Returns a result object along with the new member information.</response>        
        /// <response code="400">BadRequest. Category could not be created.</response>    
        /// <response code="401">Unauthorized. Invalid JWT Token or it wasn't provided.</response>    
        /// <response code="500">Internal Server Error.</response>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(Result<PagedResponse<ActivityDTOForDisplay>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromForm] ActivityDTOForRegister dto)
        {
            var result = await _activitiesService.Insert(dto);

            return StatusCode(result.StatusCode, result);          
        }

        /// PUT: activities/Put
        /// <summary>
        ///     Update an already created Activity
        /// </summary>
        /// <param name="activitiesDto">New value for the Activity</param>
        /// <param name="id">Id from Activity for changes</param>
        /// <response code="500">Internal Server Error</response>
        /// <response code="200">Ok. Return the new Activity updated</response>
        /// <response code="400">BadRequest.Value is empty or cant find an Activity with this Id</response>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Result<PagedResponse<ActivityDTOForDisplay>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, [FromForm] ActivitiesDtoForUpload activitiesDto)
        {
            var result = await _activitiesService.Update(id, activitiesDto);

            return StatusCode(result.StatusCode, result);
        }

        /*// DELETE api/<ActivitiesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }*/
    }
}