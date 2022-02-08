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
    [Authorize]
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

        [HttpGet(":id")]
        public async Task<Result> GetSlide(int slideId)
        {
            return await _slideSerivice.GetById(slideId);    
        }

        [HttpDelete(":id")]
        public async Task<Result> DeleteSlide(int slideId)
        {
            return await _slideSerivice.Delete(slideId);
        }

        [HttpPost]
        public async Task<Result> PostSlide(SlideDTO slideDto)
        {
            return await _slideSerivice.Insert(slideDto);
        }
    }
}