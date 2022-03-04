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
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Http;

namespace OngProject.Controllers
{
    [SwaggerTag("Auth", "Controller to register, login and get account details")]
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        public AuthController(IUserService userService)
        {
            this._userService = userService;
        }

        /// POST: auth/login
        /// <summary>
        ///     Login to enter the system.
        /// </summary>
        /// <remarks>
        ///     when you log in you will have the possibility of accessing new functionalities.
        /// </remarks>
        /// <param name="userLoginDto">Email and password of the user.</param>
        /// <response code="500">Internal Server Error.</response>              
        /// <response code="200">OK. Return an object Result returns a result object along with a generated token to login.</response>        
        /// <response code="400">BadRequest. User could not be created.</response>  
        [HttpPost]
        [Route("login")]       
        [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(Result<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromForm]  UserLoginDTO userLoginDto)
        {
            var result = await _userService.LoginAsync(userLoginDto);
            if (result.Success)
            {
                return Ok(result);
            }

            return StatusCode(result.isError() ? 500 : 400, result);
        }

        /// POST: auth/register
        /// <summary>
        ///     Create a new User.
        /// </summary>
        /// <remarks>
        ///     Add a new user who will have the possibility of accessing new functionalities.
        /// </remarks>
        /// <param name="dto">New User.</param>
        /// <response code="500">Internal Server Error.</response>              
        /// <response code="200">OK. Return an object Result returns a result object along with a generated token to login.</response>        
        /// <response code="400">BadRequest. User could not be created.</response>  
        [HttpPost]
        [Route("register")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(Result<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromForm] UserRegisterDto dto)
        {
            var result = await _userService.Insert(dto);
            if (result.Success)
            {
                return Ok(result);
            }

            return StatusCode(result.isError() ? 500 : 400, result);
        }

        /// GET: auth/me
        /// <summary>
        ///     User account detail.
        /// </summary>
        /// <remarks>
        ///     User information stored in the databaseies.
        /// </remarks>
        /// <response code="500">Internal Server Error.</response>              
        /// <response code="200">OK. Returns user account detail.</response>        
        /// <response code="400">BadRequest. User could not be created.</response> 
        [HttpGet]
        [Route("me")]
        [Authorize]
        [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(Result<UserDetailDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Me()
        {
            try
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

                return StatusCode(result.isError() ? 500 : 400, result);

            }
            catch (Exception e)
            {
                return StatusCode(500, Result.ErrorResult(new List<string> { e.Message }));
            }

        }
    }
}
