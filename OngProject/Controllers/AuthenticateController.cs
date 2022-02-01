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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        public AuthenticateController(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
            
        }
        [HttpPost]
        [HttpPost("auth/login")]
        public async Task<ActionResult> Login([FromBody] UserLoginDTO userLoginDto)
        {

            try
            {
                var currentUser = await _userManager.FindByEmailAsync(userLoginDto.Email);
                if (currentUser != null && await _userManager.CheckPasswordAsync(currentUser, userLoginDto.Password))

                {

                    var key = Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]);
                    var authClaims = new List<Claim>
                    {new Claim(ClaimTypes.Email, currentUser.Email),
                      new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())};
                    var authorizationSigninKey = new SymmetricSecurityKey(key);
                    var token = new JwtSecurityToken(
                        issuer: _configuration["JWT:ValidIssuer"],
                        audience: _configuration["JWT:ValidAudience"],
                        expires: DateTime.Now.AddHours(4),
                        claims: authClaims,
                        signingCredentials: new SigningCredentials(authorizationSigninKey, SecurityAlgorithms.HmacSha256)
                        );
                    return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token), expiration = token.ValidTo });
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }


        }


    }
}
