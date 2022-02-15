using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs;
using System;
using OngProject.Core.Models.PagedResourceParameters;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Http;

namespace OngProject.Controllers
{
    [SwaggerTag("Testimonials", "Controller to get all the testimonials by page, to post one and delete it.")]
    [Route("testimonials")]
    [ApiController]
    [Authorize]
    public class TestimonialsController : ControllerBase
    {
        private readonly ITestimonialsService _testimonialsService;
        public TestimonialsController(ITestimonialsService testimonialsService)
        {
            _testimonialsService = testimonialsService;
        }

        /// GET: /testimonials
        /// <summary>
        ///     User testimonials.
        /// </summary>
        /// <remarks>
        ///     User testimonials about how the ONG helped them.
        /// </remarks>
        /// <param name="pagingParams">Pagination parameters to display the testimonials by pages.</param>
        /// <response code="500">Internal Server Error.</response>              
        /// <response code="200">OK. Return an object Result that includes the testimonials.</response>        
        /// <response code="400">BadRequest. Return an object Result with an error message that indicate the cause of the problem.</response>  
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll([FromQuery] PaginationParams pagingParams)
        {
            var result = await _testimonialsService.GetAll(pagingParams);

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok();
        }

        /// POST: /testimonials
        /// <summary>
        ///     To add a new Testimonial.
        /// </summary>
        /// <remarks>
        ///     New Testimonial from a user.
        /// </remarks>
        /// <param name="testimonialDTO">Testimonial to save in the database.</param>           
        /// <response code="200">OK. Return an object Result that include the testimonial just added.</response>        
        /// <response code="400">BadRequest. Return an object Result with an error message that indicate the cause of the problem.</response>  
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] TestimonialDTO testimonialDTO)
        {
            try
            {
                var result = await _testimonialsService.Insert(testimonialDTO);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// PUT: /testimonials/id
        /// <summary>
        ///     Updates a testimonial.
        /// </summary>
        /// <remarks>
        ///     Updates a Testimonial from the database.
        /// </remarks>
        /// <param name="id">Id of the object to update.</param>         
        /// <response code="500">Internal Server Error.</response>              
        /// <response code="200">OK. Return an object Result indicating that the testimonial was updated in the Db.</response>        
        /// <response code="404">NotFound. Return an object Result with an error message that indicate the cause of the problem.</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromForm] TestimonialDTO dto)
        {
            var response = await _testimonialsService.Update(id,dto);

            return StatusCode(response.StatusCode, response);
        }

        /// DELETE: /testimonials/id
        /// <summary>
        ///     Deletes a Testimonial.
        /// </summary>
        /// <remarks>
        ///     Deletes a Testimonial from the database.
        /// </remarks>
        /// <param name="id">Id of the object to delete.</param>         
        /// <response code="500">Internal Server Error.</response>              
        /// <response code="200">OK. Return an object Result indicating that the testimonial was deleted in the Db.</response>        
        /// <response code="400">BadRequest. Return an object Result with an error message that indicate the cause of the problem.</response>  
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _testimonialsService.Delete(id);
            if (response.Success)
                return Ok(response);
            return StatusCode(response.isError() ? 500 : 404, response);
        }
    }
}
