using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

        // GET api/<ActivitiesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok();
        }

        /// POST: activities/Post
        /// <summary>
        ///     Create a new Activity
        /// </summary>
        /// <param name="dto">New Activity</param>
        /// <response code="500">Internal Server Error</response>
        /// <response code="200">Ok. Return the new Activity created</response>
        /// <response code="400">BadRequest.Name or Content of the new Activity is already exist</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromForm] ActivityDTOForRegister dto)
        {
            try
            {
                
                var result = await _activitiesService.Insert(dto);

                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        /// PUT: activities/Put
        /// <summary>
        ///     Update an already created Activity
        /// </summary>
        /// <param name="value">New value for the Activity</param>
        /// <param name="id">Id from Activity for changes</param>
        /// <response code="500">Internal Server Error</response>
        /// <response code="200">Ok. Return the new Activity updated</response>
        /// <response code="400">BadRequest.Value is empty or cant find an Activity with this Id</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ActivitiesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
