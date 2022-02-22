using OngProject.Controllers;
using Test.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OngProject.Core.Business;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Models.Response;
using OngProject.Core.Models.DTOs;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.IO;
using Moq;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Test
{
    [TestClass]
    public class UserControllerTest
    {

        [ClassInitialize()]
        public static void Setup(TestContext testContext)
        {
            ContextHelper.MakeDbContext();
        }

        [TestMethod]
        public async Task DeleteUserSuccessTest()
        {
            // Arrange
            var userService = new UsersService(ContextHelper.UnitOfWork, ContextHelper.Config, ContextHelper.JwtHelper, new ImageService(ContextHelper.UnitOfWork), ContextHelper.EntityMapper);            
            var userController = new UserController(userService);            
            var user = await ContextHelper.UnitOfWork.UserRepository.GetByIdAsync(2);
            
            // Act
            var response = await userController.Delete(user.Id);
            var result = response as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsTrue(user.SoftDelete);            
        }        

        [TestMethod]
        public async Task DeleteUserFailUserNotExistTest()
        {
            // Arrange
            var userService = new UsersService(ContextHelper.UnitOfWork, ContextHelper.Config, ContextHelper.JwtHelper, new ImageService(ContextHelper.UnitOfWork), ContextHelper.EntityMapper);
            var userController = new UserController(userService);            

            // Act
            var response = await userController.Delete(50);            
            var result = response as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(404, result.StatusCode);
        }

        [TestMethod]
        public async Task DeleteUserFailUserAlreadyDeletedTest()
        {
            // Arrange
            var userService = new UsersService(ContextHelper.UnitOfWork, ContextHelper.Config, ContextHelper.JwtHelper, new ImageService(ContextHelper.UnitOfWork), ContextHelper.EntityMapper);
            var userController = new UserController(userService);

            // Act
            var response = await userController.Delete(1); // seed de usuario 1 se crea eliminado
            var result = response as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(404, result.StatusCode);
        }

        [TestMethod]
        public async Task GetAllUsersSuccessTest()
        {
            // Arrange
            ContextHelper.MakeDbContext();
            var userService = new UsersService(ContextHelper.UnitOfWork, ContextHelper.Config, ContextHelper.JwtHelper, new ImageService(ContextHelper.UnitOfWork), ContextHelper.EntityMapper);
            var userController = new UserController(userService);                       

            // Act
            var response = await userController.Get();
            var objectResult = response as ObjectResult;
            var result = objectResult.Value as Result<ICollection<UserDTO>>;

            // Assert
            Assert.IsNotNull(objectResult);
            Assert.AreEqual(200, objectResult.StatusCode);
            Assert.IsTrue(result.Success);
            Assert.IsTrue(result.Data.Count > 0);
        }

        [TestMethod]
        public async Task GetAllUsersFailThereAreNoUsersTest()
        {
            // Arrange
            ContextHelper.MakeDbContext(false); // reinicializar DB sin datos
            var userService = new UsersService(ContextHelper.UnitOfWork, ContextHelper.Config, ContextHelper.JwtHelper, new ImageService(ContextHelper.UnitOfWork), ContextHelper.EntityMapper);
            var userController = new UserController(userService);

            // Act
            var response = await userController.Get();
            var objectResult = response as ObjectResult;            

            // Assert
            Assert.IsNotNull(objectResult);
            Assert.AreEqual(404, objectResult.StatusCode);            
        }

        [TestMethod]
        public async Task PutUserSuccessfullyTest()
        {
            //Arrange
            var userDto = new UserUpdateDto()
            {
                FirstName = "testFirstname",
                LastName = "testLastname",
                Email = "testUser@ong.com",
                Photo = CreateImage()
            };
            var mockImageService = new Mock<ImageService>(ContextHelper.UnitOfWork);
            mockImageService.Setup(x => x.AwsDeleteFile(It.IsAny<string>())).ReturnsAsync("");
            mockImageService.Setup(x => x.UploadFile(It.IsAny<string>(), It.IsAny<IFormFile>())).ReturnsAsync("https://myTestBucket.com/userTet/imageName.png");

            var userService = new UsersService(ContextHelper.UnitOfWork, ContextHelper.Config, ContextHelper.JwtHelper, mockImageService.Object, ContextHelper.EntityMapper);
            var userController = new UserController(userService);
            userController.ControllerContext = await PrepareLoginContextHelper.GetLoginContext("User12@ong.com", "Password12");

            //Act
            var response = await userController.Put(userDto);
            var result = response as ObjectResult;
            var expectedResult = result.Value as Result<UserDtoForDisplay>;

            //Assert
            Assert.IsNotNull(response);
            Assert.IsNotNull(result);
            Assert.IsNotNull(expectedResult);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsInstanceOfType(expectedResult.Data, typeof(UserDtoForDisplay));
        }

        [TestMethod]
        public async Task PutUserFailRequiredFirstNameTest()
        {
            //Arrange
            var userDto = new UserUpdateDto()
            {
                FirstName = "",
                LastName = "testLastname",
                Email = "testUser@ong.com",
                Photo = CreateImage()
            };

            //Act
            var errorcount = checkValidationProperties(userDto).Count;

            //Assert
            Assert.AreNotEqual(0, errorcount);
        }

        [TestMethod]
        public async Task PutUserFailRequiredLastNameTest()
        {
            //Arrange
            var userDto = new UserUpdateDto()
            {
                FirstName = "testFirstName",
                LastName = "",
                Email = "testUser@ong.com",
                Photo = CreateImage()
            };

            //Act
            var errorcount = checkValidationProperties(userDto).Count;

            //Assert
            Assert.AreNotEqual(0, errorcount);
        }

        [TestMethod]
        public async Task PutUserFailRequiredEmailTest()
        {
            //Arrange
            var userDto = new UserUpdateDto()
            {
                FirstName = "testFirstName",
                LastName = "testLastname",
                Email = "",
                Photo = CreateImage()
            };

            //Act
            var errorcount = checkValidationProperties(userDto).Count;

            //Assert
            Assert.AreNotEqual(0, errorcount);
        }

        [TestMethod]
        public async Task PutUserFailInvalidFormatEmailTest()
        {
            //Arrange
            var userDto = new UserUpdateDto()
            {
                FirstName = "testFirstName",
                LastName = "testLastname",
                Email = "sdfa@",
                Photo = CreateImage()
            };

            //Act
            var errorcount = checkValidationProperties(userDto).Count;

            //Assert
            Assert.AreNotEqual(0, errorcount);
        }

        [TestMethod]
        public async Task PutUserFailRequiredPhotoTest()
        {
            //Arrange
            var userDto = new UserUpdateDto()
            {
                FirstName = "testFirstName",
                LastName = "testLastname",
                Email = "testUser@ong.com"
            };

            //Act
            var errorcount = checkValidationProperties(userDto).Count;

            //Assert
            Assert.AreNotEqual(0, errorcount);
        }

        public IFormFile CreateImage()
        {
            //cambiar la ubicacion de la imagen para que detecte el archivo si se ejecuta desde otra pc
            var stream = File.OpenRead(@$"{Environment.CurrentDirectory}\Test\Image\Captura1.png");
            var image = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name))
            {
                Headers = new HeaderDictionary(),
                ContentType = "image/png"
            };
            return image;
        }
        
        public IList<ValidationResult> checkValidationProperties(object model)
        {
            var result = new List<ValidationResult>();
            var validationContext = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, validationContext, result, true);
            if (model is IValidatableObject) (model as IValidatableObject).Validate(validationContext);            

            return result;
        }
    }
}
