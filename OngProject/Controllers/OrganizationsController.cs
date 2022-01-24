using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OngProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationsController : ControllerBase
    {
        private readonly IOrganizationsService _organizationsService;

        public OrganizationsController(IOrganizationsService organizationsService)
        {
            _organizationsService = organizationsService;
        }

        // GET: api/<OrganizationsController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
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