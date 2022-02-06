using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using OngProject.Core.Models.DTOs;
using OngProject.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Security.Claims;
using OngProject.Core.Models.Response;
using System.Collections.Generic;

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

        [HttpGet]
        [Route("me")]
        [Authorize]
        public async Task<IActionResult> Me()
        {   try
            {
                var claimId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                if (claimId == null)
                {
                    throw new Exception("No se encontró id de usuario");
                }

                var result = await this._userService.GetById(Int32.Parse(claimId.Value));
                if (result.Success)
                {
                    return Ok(result);
                }

                return BadRequest(result);
                
            }catch(Exception e)
            {
                return StatusCode(500, Result.ErrorResult(new List<string> { e.Message }));
            }

        }
    }
}
