using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OngProject.Controllers;
using OngProject.Core.Business;
using OngProject.Core.Models.DTOs;
using OngProject.Core.Models.Paged;
using OngProject.Core.Models.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Helper;

namespace Test
{
    [TestClass]
    public class ContactTest
    {
        [ClassInitialize()]

        public static void Setup(TestContext testContext)
        {
            ContextHelper.MakeDbContext();
        }

        [TestMethod]
        public async Task GetAllSuccessTest()
        {
            // Arrange
            /*HostString myUri = new("www.api.com:8080");
            var MockContactHttpContex = new Mock<HttpContext>();
            MockContactHttpContex.Setup(x => x.Request.Scheme).Returns("https");
            MockContactHttpContex.Setup(x => x.Request.Host).Returns(myUri);
            MockContactHttpContex.Setup(x => x.Request.Path).Returns("/Contact");

            ContextHelper.httpContext.HttpContext = MockContactHttpContex.Object;*/

            ContextHelper.MakeDbContext();

            var contactService = new ContactService(ContextHelper.Config, ContextHelper.UnitOfWork, ContextHelper.EntityMapper);
            var contactController = new ContactController(contactService);

            //Act
            var response = await contactController.GetAllContacts();
            var objectResult = response as ObjectResult;
            var result = objectResult.Value as IEnumerable<ContactDTO>;

            //Assert
            Assert.IsNotNull(objectResult);
            Assert.AreEqual(200, objectResult.StatusCode);
            Assert.IsTrue(result.ToList().Count > 0);
        }

        [TestMethod]
        public async Task AddContactSuccessTest()
        {
            // Arrange
            ContextHelper.MakeContext();

            var contactDto = new ContactDTO()
            {
                Email = "User12@ong.com",
                Message = "Mensaje de Prueba",
                Name = "Nombre de prueba",
                Phone = "12345678"
            };

            var contactService = new ContactService(ContextHelper.Config, ContextHelper.UnitOfWork, ContextHelper.EntityMapper);
            var contactController = new ContactController(contactService);

            // Act
            var response = await contactController.Post(contactDto);
            var result = response as ObjectResult;
            var resultDto = result.Value as Result<ContactDTO>;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(resultDto);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsInstanceOfType(resultDto.Data, typeof(ContactDTO));
        }

        [TestMethod]
        public async Task AddContactWithNullNameFailedTest()
        {
            // Arrange
            var contactDto = new ContactDTO()
            {
                Name = null,
                Email = "mailDePrueba@prueba.com",
                Message = "Mensaje de Prueba",
                Phone = "12345678"
            };

            var contactService = new ContactService(ContextHelper.Config, ContextHelper.UnitOfWork, ContextHelper.EntityMapper);
            var contactController = new ContactController(contactService);

            // Act
            var response = await contactController.Post(contactDto);
            var result = response as ObjectResult;

            // Assert
            Assert.AreNotEqual(0, result.StatusCode);
        }

        [TestMethod]
        public async Task AddContactWithNullEmailFailedTest()
        {
            // Arrange
            var contactDto = new ContactDTO()
            {
                Name = "Nombre de prueba",
                Email = null,
                Message = "Mensaje de Prueba",
                Phone = "12345678"
            };

            var contactService = new ContactService(ContextHelper.Config, ContextHelper.UnitOfWork, ContextHelper.EntityMapper);
            var contactController = new ContactController(contactService);

            // Act
            var response = await contactController.Post(contactDto);
            var result = response as ObjectResult;

            // Assert
            Assert.AreNotEqual(0, result.StatusCode);
        }

    }
}
