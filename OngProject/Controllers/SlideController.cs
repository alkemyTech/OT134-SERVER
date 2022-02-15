using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs;
using OngProject.Core.Models.Response;
using System;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    [Route("/Slides/")]
    [ApiController]
    [Authorize(Roles = "Administrator")]
    public class SlideController : Controller
    {
        private readonly ISlideSerivice _slideSerivice;

        public SlideController(ISlideSerivice slideSerivice)
        {
            _slideSerivice = slideSerivice;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSlides()
        {
            try
            {
                var slides = await _slideSerivice.GetAll();
                if (slides != null)
                    return Ok(slides);
                else
                    return NotFound("No se encontraron Slides");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSlide(int id)
        {
            var result = await _slideSerivice.GetById(id);
            if (result.Success)
                return Ok(result);
            return StatusCode(result.isError()? 500 : 404,result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSlide(int id)
        {
            var result = await _slideSerivice.Delete(id);
            if (result.Success)
                return Ok(result);
            return StatusCode(result.isError() ? 500 : 400, result);
        }

        [HttpPost]
        public async Task<IActionResult> PostSlide(SlideDtoForRegister slideDto)
        {
            var result = await _slideSerivice.Insert(slideDto);
            
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromForm] SlideDtoForUpdate dto)
        {
            var result = await _slideSerivice.Update(id, dto);

            return StatusCode(result.StatusCode, result);
        }
    }
}