using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    [Route("users")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/<UserController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            
            var results = await _userService.GetAll();

            if (results.Success)
            {
                return Ok(results);
            }

            return BadRequest(results);            
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok();
        }

        // POST api/<UserController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT <UserController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromForm] UserUpdateDto user)
        {
            var response = await _userService.Update(id, user);
            return StatusCode(response.StatusCode, response);
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int id)
        {            
            var result = await this._userService.Delete(id);
            if (result.Success)
            {
                return Ok(result);
            }
         
            return BadRequest(result);         
        }
    }
}

