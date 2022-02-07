using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using OngProject.Core.Models.DTOs;
using OngProject.Core.Interfaces;

namespace OngProject.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        public AuthController(IUserService userService)
        {
            this._userService = userService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO userLoginDto)
        {            
            var result = await _userService.LoginAsync(userLoginDto);
            if (result.Success)
            {
                return Ok(result);
            }
            
            return BadRequest(result);            
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto dto)
        {
            var result = await _userService.Insert(dto);
            if (result.Success)
            {
                return Ok(result);                
            }
            
            return BadRequest(result);
        }
        
    }
}
