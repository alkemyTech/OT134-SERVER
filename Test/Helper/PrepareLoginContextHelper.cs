using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Controllers;
using OngProject.Core.Business;
using OngProject.Core.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Test.Helper
{
    public class PrepareLoginContextHelper
    {
        public static async Task<ControllerContext> GetLoginContext(string email, string password)
        {
            var loginDTO = new UserLoginDTO
            {
                Email = email,
                Password = password
            };

            var userService = new UsersService(ContextHelper.UnitOfWork, ContextHelper.Config, ContextHelper.JwtHelper, new ImageService(ContextHelper.UnitOfWork), ContextHelper.EntityMapper);
            var loginController = new AuthController(userService);

            var loginResponse = await loginController.Login(loginDTO);
            var r = loginResponse as OkObjectResult;
            string token = r.Value.ToString();

            var userResult = await ContextHelper.UnitOfWork.UserRepository.FindByConditionAsync(x => x.Email == email);
            var user = userResult.FirstOrDefault();

            var userClaim = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                                        new Claim(ClaimTypes.Name, user.Email),
                                        new Claim(ClaimTypes.Role, user.Rol?.Name)
                                   }, "TestAuthentication"));

            var httpContext = new DefaultHttpContext() { User = userClaim };
            httpContext.Request.Headers.Add("Authorization", $"Bearer {token}");

            return new ControllerContext()
            {
                HttpContext = httpContext,
            };
        }
    }
}
