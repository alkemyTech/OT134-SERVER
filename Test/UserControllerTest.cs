using OngProject.Controllers;
using Test.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OngProject.Core.Business;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Models.Response;
using OngProject.Core.Models.DTOs;
using System.Collections.Generic;

namespace Test
{
    [TestClass]
    public class UserControllerTest
    {
        [ClassInitialize()]
        public static void Setup(TestContext testContext)
        {
            // ContextHelper.MakeDbContext();
        }

        [TestMethod]
        public async Task DeleteUserSuccessfullyTest()
        {
            // Arrange            
            var userService = new UsersService(ContextHelper.UnitOfWork, ContextHelper.Config, ContextHelper.JwtHelper, new ImageService(ContextHelper.UnitOfWork), ContextHelper.EntityMapper);
            var userController = new UserController(userService);
            var user = await ContextHelper.UnitOfWork.UserRepository.GetByIdAsync(1);
            
            // Act
            var response = await userController.Delete(user.Id);
            var result = response as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsTrue(user.SoftDelete);            
        }

        [TestMethod]
        public async Task DeleteUserWhenUserNotExistTest()
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
        public async Task GetAllUsersSuccessfullyTest()
        {
            // Arrange
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
        public async Task GetAllUsersWhenThereAreNoUsersTest()
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
    }
}
