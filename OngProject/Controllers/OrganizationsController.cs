using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OngProject.Controllers
{
    [Route("api/organization")]
    [ApiController]
    public class OrganizationsController : ControllerBase
    {
        private readonly IOrganizationsService _organizationsService;

        public OrganizationsController(IOrganizationsService organizationsService)
        {
            _organizationsService = organizationsService;
        }

        // GET: api/organization/public
        [HttpGet, Route("public")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var org = await _organizationsService.GetAll();
                if (org != null)
                    return Ok(new OrganizationDTO
                    {
                        Name = org.Name,
                        Image = org.Image,
                        Address = org.Address,
                        Phone = org.Phone
                    });
                else
                    return NotFound("No se encontró información sobre la organización");            
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET api/<OrganizationsController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok();
        }

        // POST api/<OrganizationsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<OrganizationsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<OrganizationsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}