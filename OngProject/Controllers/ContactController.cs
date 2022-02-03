using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs;
using System;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    [Route("contacts")]
    [ApiController]
    [Authorize]
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllSlides()
        {
            try
            {
                var contacts = await _contactService.GetAll();
                if (contacts != null)
                    return Ok(contacts);
                else
                    return NotFound("No se encontraron Contactos");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post(ContactDTO contactDto)
        {
            var response = await _contactService.Insert(contactDto);
            return StatusCode(response.StatusCode, response.Message);
        }
    }
}
