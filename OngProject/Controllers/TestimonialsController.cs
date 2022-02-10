using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs;
using System;
using OngProject.Core.Models.PagedResourceParameters;

namespace OngProject.Controllers
{
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
        
        [HttpGet]
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
        
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }
        
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
