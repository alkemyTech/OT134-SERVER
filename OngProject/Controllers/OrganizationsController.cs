using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs;
using System;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OngProject.Controllers
{
    [Route("organizations")]
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
                var orgDto = await _organizationsService.GetAll();
                if (orgDto != null)
                    return Ok(orgDto);
                else
                    return NotFound("No se encontró información sobre la organización");            
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // POST api/<OrganizationsController>
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Post([FromForm] OrganizationDTOForUpload organizationDTOForUpload)
        {
            try
            {
                var result = await _organizationsService.Insert(organizationDTOForUpload);
                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<OrganizationsController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Put(int id,[FromForm] OrganizationDTOForUpload organizationDTOForUpload)
        {
            try
            {
                var result = await _organizationsService.Update(id, organizationDTOForUpload);
                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}