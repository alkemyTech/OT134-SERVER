using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OngProject.Controllers;
using OngProject.Core.Business;
using OngProject.Core.Models.DTOs;
using OngProject.Core.Models.Response;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Helper;

namespace Test
{
    [TestClass]
    public class OrganizationsControllerTest
    {
        [ClassInitialize()]

        public static void Setup(TestContext testContext)
        {
            ContextHelper.MakeDbContext();
        }
        

        [TestMethod]
        public async Task AddOrganizationSuccesfullyTest()
        {
            //Arrange
            var Image = CreateImage();

            var organizationDTO = new OrganizationDTOForUpload
            {
                Name = "testName",
                AboutUsText = "About us test text",
                Address = "Adress test",
                FacebookUrl = "FacebookUrl",
                Email = "Email",
                Image = Image,
                InstagramUrl = "InstagramUrl",
                LinkedinUrl = "LinkedinUrl",
                Phone = 213123123,
                WelcomeText = "Welcome text test",

            };

            var mockImageService = new Mock<ImageService>(ContextHelper.UnitOfWork);
            mockImageService.Setup(x => x.AwsDeleteFile(It.IsAny<string>())).ReturnsAsync("");
            mockImageService.Setup(x => x.UploadFile(It.IsAny<string>(), It.IsAny<IFormFile>())).ReturnsAsync("https://myTestONG.com/imageName.png");

            var organizationService = new OrganizationService(ContextHelper.UnitOfWork, ContextHelper.Config, ContextHelper.JwtHelper, ContextHelper.EntityMapper, ContextHelper.Slide, mockImageService.Object);
            var organizationController = new OrganizationsController(organizationService);

            //Act

            var response = await organizationController.Post(organizationDTO);
            var result = response as ObjectResult;
            var resultDTO = result.Value as Result<OrganizationDTOForDisplay>;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(resultDTO);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsInstanceOfType(resultDTO.Data, typeof(OrganizationDTOForDisplay));

        }

        [TestMethod]
        public async Task AddOrganizationWithNullNameFailedTest()
        {
            //Arrange
            var Image = CreateImage();

            var organizationDTO = new OrganizationDTOForUpload
            {
                Name = null,
                AboutUsText = "About us test text",
                Address = "Adress test",
                FacebookUrl = "FacebookUrl",
                Email = "Email",
                Image = Image,
                InstagramUrl = "InstagramUrl",
                LinkedinUrl = "LinkedinUrl",
                Phone = 213123123,
                WelcomeText = "Welcome text test",

            };
            var mockImageService = new Mock<ImageService>(ContextHelper.UnitOfWork);
            mockImageService.Setup(x => x.AwsDeleteFile(It.IsAny<string>())).ReturnsAsync("");
            mockImageService.Setup(x => x.UploadFile(It.IsAny<string>(), It.IsAny<IFormFile>())).ReturnsAsync("https://myTestONG.com/imageName.png");
            var organizationService = new OrganizationService(ContextHelper.UnitOfWork, ContextHelper.Config, ContextHelper.JwtHelper, ContextHelper.EntityMapper, ContextHelper.Slide, new ImageService(ContextHelper.UnitOfWork));
            var organizationController = new OrganizationsController(organizationService);

            //Act

            var response = await organizationController.Post(organizationDTO);
            var result = response as ObjectResult;

            //Assert
            Assert.AreNotEqual(0, result.StatusCode);

        }

        [TestMethod]
        public async Task AddOrganizationWithNullInstagramUrlTest()
        {
            //Arrange
            var Image = CreateImage();

            var organizationDTO = new OrganizationDTOForUpload
            {
                Name = null,
                AboutUsText = "About us test text",
                Address = "Adress test",
                FacebookUrl = "FacebookUrl",
                Email = "Email",
                Image = Image,
                InstagramUrl = null,
                LinkedinUrl = "LinkedinUrl",
                Phone = 213123123,
                WelcomeText = "Welcome text test",

            };
            var mockImageService = new Mock<ImageService>(ContextHelper.UnitOfWork);
            mockImageService.Setup(x => x.AwsDeleteFile(It.IsAny<string>())).ReturnsAsync("");
            mockImageService.Setup(x => x.UploadFile(It.IsAny<string>(), It.IsAny<IFormFile>())).ReturnsAsync("https://myTestONG.com/imageName.png");
            var organizationService = new OrganizationService(ContextHelper.UnitOfWork, ContextHelper.Config, ContextHelper.JwtHelper, ContextHelper.EntityMapper, ContextHelper.Slide, new ImageService(ContextHelper.UnitOfWork));
            var organizationController = new OrganizationsController(organizationService);

            //Act

            var response = await organizationController.Post(organizationDTO);
            var result = response as ObjectResult;

            //Assert
            Assert.AreNotEqual(0, result.StatusCode);

        }

        [TestMethod]

        public async Task PutCategorySuccesfullyTest()
        {
            //Arrange
            ContextHelper.MakeDbContext();
            var Image = CreateImage();

            var organizationDTO = new OrganizationDTOForUpload()
            {
                Name = "testName",
                AboutUsText = "About us test text",
                Address = "Adress test",
                FacebookUrl = "FacebookUrl",
                Email = "Email",
                Image = Image,
                InstagramUrl = "InstagramUrl",
                LinkedinUrl = "LinkedinUrl",
                Phone = 213123123,
                WelcomeText = "Welcome text test",
            };

            var mockImageService = new Mock<ImageService>(ContextHelper.UnitOfWork);
            mockImageService.Setup(x => x.AwsDeleteFile(It.IsAny<string>())).ReturnsAsync("");
            mockImageService.Setup(x => x.UploadFile(It.IsAny<string>(), It.IsAny<IFormFile>())).ReturnsAsync("https://myTestONG.com/imageName.png");

            var organizationService = new OrganizationService(ContextHelper.UnitOfWork, ContextHelper.Config, ContextHelper.JwtHelper, ContextHelper.EntityMapper, ContextHelper.Slide, mockImageService.Object);
            var organizationController = new OrganizationsController(organizationService);

            //Act
            var response = await organizationController.Put( 1, organizationDTO);
            var result = response as ObjectResult;
            var resultDTO = result.Value as Result<OrganizationDTOForDisplay>;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(resultDTO);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsInstanceOfType(resultDTO.Data, typeof(OrganizationDTOForDisplay));
        }


        [TestMethod]

        public async Task GetAllOrganizationsSuccessfullyTest()
        {

            //Arrange
            ContextHelper.MakeDbContext();
            var organizationService = new OrganizationService(ContextHelper.UnitOfWork, ContextHelper.Config, ContextHelper.JwtHelper,  ContextHelper.EntityMapper, ContextHelper.Slide, new ImageService(ContextHelper.UnitOfWork));
            var organizationController = new OrganizationsController(organizationService);

            //Act
            var response = await organizationController.Get();
            var objectResult = response as ObjectResult;
            var result = objectResult.Value as IEnumerable<OrganizationDTOForDisplay>;

            //Assert
            Assert.IsNotNull(objectResult);
            Assert.AreEqual(200, objectResult.StatusCode);
            Assert.IsTrue(result.ToList().Count > 0);
        }


        [TestMethod]

        public async Task GetAllOrganizationsWhenThereAreNoOrganizationsTest()
        {
            //Arrange
            var organizationService = new OrganizationService(ContextHelper.UnitOfWork, ContextHelper.Config, ContextHelper.JwtHelper, ContextHelper.EntityMapper, ContextHelper.Slide, new ImageService(ContextHelper.UnitOfWork));
            var organizationController = new OrganizationsController(organizationService);

            //Act
            ContextHelper.MakeDbContext(false);
            var response = await organizationController.Get();
            var result = response as ObjectResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(404, result.StatusCode);

        }

        public IFormFile CreateImage()
        {
            //siempre cambiar la ubicacion de la imagen para que detecte el archivo
            var stream = File.OpenRead(@"C:\Users\Haru\Desktop\4.png");
            var image = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name))
            {
                Headers = new HeaderDictionary(),
                ContentType = "image/png"
            };
            return image;
        }

    }
}

