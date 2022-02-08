using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.Response;
using OngProject.Core.Models.DTOs;
using System;

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
        public IActionResult Get()
        {
            return Ok();
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
        public async Task<Result> Delete(int id)
        {
            return await _testimonialsService.Delete(id);
        }
    }
}
