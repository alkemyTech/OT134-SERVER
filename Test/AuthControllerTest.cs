using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OngProject.Controllers;
using OngProject.Core.Business;
using OngProject.Core.Models.DTOs;
using OngProject.Repositories;
using OngProject.Core.Helper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Test.Helper;
using OngProject.Core.Interfaces;
using OngProject.Repositories.Interfaces;
using OngProject.DataAccess;
using System.IO;
using Microsoft.AspNetCore.Http;
using OngProject.Core.Models.Response;
using System.Security.Claims;

namespace Test
{
    [TestClass]
    public class AuthControllerTest
    {
        private AppDbContext _context;
        private IUnitOfWork unitOfWork;
        private IConfiguration configurations;
        private JwtHelper jwtHelper;
        private IUserService userService;

        [TestInitialize]
        public void Init()
        {
            var configcollection = new Dictionary<string, string>()
            {
                {"SendGridAPIKey",""},
                {"JWT:Secret","123456789123456789" }
            };
            configurations = new ConfigurationBuilder()
                .AddInMemoryCollection(configcollection)
                .Build();

            _context = PrepareDbContextHelper.MakeDbContext(true);
            unitOfWork = new UnitOfWork(_context);

            jwtHelper = new JwtHelper(configurations);
            userService = new UsersService(unitOfWork, configurations, jwtHelper, new ImageService(ContextHelper.UnitOfWork), ContextHelper.EntityMapper);
        }

        [TestMethod]
        [DataRow("User1@ong.com", "Password1", 200)] // Login succesfull.
        [DataRow("T@gmail.com", "Password1", 400)] // Email doesn't exists.
        [DataRow("User1@ong.com", "123456789", 400)] // Wrong password.
        public async Task LoginTestInitSessionEmailAndPassword(string email, string password, int expected)
        {
            using (_context)
            {
                //Arrange
                var loginDTO = new UserLoginDTO
                {
                    Email = email,
                    Password = password
                };
                var loginController = new AuthController(userService);

                //Act
                var login = await loginController.Login(loginDTO);
                var actual = login as ObjectResult;

                //Assert
                Assert.IsNotNull(actual);
                Assert.AreEqual(expected, actual.StatusCode);
            }
        }

        [TestMethod]
        [DataRow("mflopezartes@gmail.com", "123456", 200)] // Register succesfull and email sent.
        [DataRow("User1@ong.com", "123456", 400)] // Email in use.
        public async Task RegisterTestSignUpSuccessEmailInUse(string email, string password, int expected)
        {
            using (_context)
            {
                //Arrange
                var imageCurretPath = Directory.GetCurrentDirectory();
                var index = imageCurretPath.IndexOf("Test\\");
                var finalPath = imageCurretPath.Substring(0, index + 4) + "\\Image\\Captura1.png";
                var imageFile = File.OpenRead(finalPath);
                IFormFile file = new FormFile(imageFile, 0, imageFile.Length, "Captura1", "Captura1.png") 
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "image/png"
                };
               var registerDto = new UserRegisterDto
               {
                   Email = email,
                   Password = password,
                   FirstName = "Test First Name",
                   LastName = "Teste Last Name",
                   Photo = file,
                   RolId = 1
               };
               var registerController = new AuthController(userService);

               //Act
               var register = await registerController.Register(registerDto);
               var actual = register as ObjectResult;

               //Assert
               Assert.IsNotNull(actual);
               Assert.AreEqual(expected, actual.StatusCode);
            }
        }

        [TestMethod]
        public async Task GetDataUser()
        {
            using (_context)
            {
                //Arrange
                var loginDTO = new UserLoginDTO
                {
                    Email = "User1@ong.com",
                    Password = "Password1"
                };
                var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.NameIdentifier, "1"),
                    new Claim(ClaimTypes.Name, "User1@ong.com"),
                    new Claim(ClaimTypes.Role, "User")
                }, "TestAuthentication"));
                var authController = new AuthController(userService);

                //Act
                var login = await authController.Login(loginDTO);
                var r = login as OkObjectResult;
                
                Result<string> result = r.Value as Result<string>;
                var token = result.Data;

                var httpContext = new DefaultHttpContext() 
                {
                    User = user
                };
                httpContext.Request.Headers.Add("Authorization", "Bearer " + token);
                var controllerContext = new ControllerContext()
                {
                    HttpContext = httpContext,
                };

                authController.ControllerContext = controllerContext;

                var response = await authController.Me();
                var expected = response as ObjectResult;

                //Assert                
                Assert.AreEqual(200, expected.StatusCode);
            }
        }
    }
}