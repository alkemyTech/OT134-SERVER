using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using OngProject.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using OngProject.Core.Models.DTOs;
using OngProject.Core.Models.PagedResourceParameters;

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
        public async Task<IActionResult> GetAllMembers([FromQuery] PaginationParams pagingParams)
        {
            var result = await _membersService.GetAll(pagingParams);

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] MemberDTORegister memberDTO)
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
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _membersService.Delete(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return StatusCode(result.isError() ? 500 : 400, result);
        }
    }
}