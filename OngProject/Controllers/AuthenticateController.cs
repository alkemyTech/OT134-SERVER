using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using OngProject.Core.Models.DTOs;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using OngProject.Entities;
using OngProject.Core.Helper;
using Microsoft.Extensions.Configuration;
using OngProject.Core.Models;

namespace OngProject.Controllers
{
    [AllowAnonymous]
    public class AuthenticateController : ControllerBase
    {
        private readonly IUserService _userService;
        public AuthenticateController(IUserService userService)
        {
            _userService = userService;
            
        }
        [HttpPost("/login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO userLoginDto)
        {

            try
            {
                var result = await _userService.LoginAsync(userLoginDto);

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
