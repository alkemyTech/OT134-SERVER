using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using OngProject.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using OngProject.Core.Models.Response;
using OngProject.Core.Models.DTOs;

namespace OngProject.Controllers
{
    [Route("members")]
    [ApiController]
    [Authorize]
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
        public async Task<IActionResult> Post([FromForm] MemberDTO memberDTO)
        {
            try
            {
                var result = await _membersService.Insert(memberDTO);

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
        public async Task<Result> Delete(int id)
        {
            return await _membersService.Delete(id);
        }
    }
}