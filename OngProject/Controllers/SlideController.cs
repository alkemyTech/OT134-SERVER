using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using System;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    public class SlideController : Controller
    {
        private readonly ISlideSerivice _slideSerivice;

        public SlideController(ISlideSerivice slideSerivice)
        {
            _slideSerivice = slideSerivice;
        }
        [Authorize]
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
    }
}