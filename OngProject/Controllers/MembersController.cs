using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using OngProject.Core.Interfaces;

namespace OngProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly IMemberService _membersService;

        public MembersController(IMemberService memberService)
        {
            _membersService = memberService;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var memberDTO = await _membersService.GetAll();
                if (memberDTO != null)
                {
                    return Ok(memberDTO);
                }
                else return NotFound("No se encontró información sobre los miembros");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok();
        }
        
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }
        
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }
        
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}