using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using System;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    [Route("contacts")]
    [ApiController]
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }
        [Authorize]
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
    }
}
