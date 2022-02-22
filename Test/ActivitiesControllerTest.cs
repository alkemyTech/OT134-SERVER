using OngProject.Controllers;
using Test.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OngProject.Core.Business;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Models.DTOs;
using System.Collections.Generic;
using OngProject.Core.Helper;
using OngProject.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using OngProject.DataAccess;
using Microsoft.EntityFrameworkCore;
using OngProject.Repositories;
using System;
using OngProject.Entities;
using OngProject.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using System.IO;
using Moq;

namespace Test
{
    [TestClass]
    public class ActivitiesControllerTest
    {
        #region Auxiliar Methods
        #region TestInit and Injections
        private AppDbContext _context;
        private IUnitOfWork unitOfWork;
        private IConfiguration configurations;
        private IJwtHelper jwtHelper;
        private IActivitiesService activitiesService;
        private ActivitiesController activitiesController;

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

            var mockImageService = new Mock<ImageService>(ContextHelper.UnitOfWork);
            var link = "https://www.netmentor.es/entrada/mock-unit-test-csharp";
            mockImageService.Setup(x => x.UploadFile(It.IsAny<string>(), It.IsAny<IFormFile>())).ReturnsAsync(link);

            _context = GetTestContext(Guid.NewGuid().ToString());
            unitOfWork = new UnitOfWork(_context);
            jwtHelper = new JwtHelper(configurations);
            activitiesService = new ActivitiesService(unitOfWork, ContextHelper.EntityMapper, mockImageService.Object);
            activitiesController = new ActivitiesController(activitiesService);
            SeedDB(_context);            
        }
        #endregion

        private AppDbContext GetTestContext(string testDb)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(testDb)
                .Options;
            var dbcontext = new AppDbContext(options);
            return dbcontext;
        }

        private void SeedDB(AppDbContext context)
        {
            var activity = new Activities
            {
                Name = "Activity",
                Content = "Content from activity",
                Image = "ActivityImage.jpg"
            };

            context.Add(activity);
            context.SaveChanges();
        }

        private IFormFile CreateImage()
        {
            var imageCurrentPath = Directory.GetCurrentDirectory();
            var index = imageCurrentPath.IndexOf("Test\\");
            var finalPath = imageCurrentPath.Substring(0, index + 4) + "\\Image\\Captura1.png";
            var imageFile = File.OpenRead(finalPath);

            IFormFile newFile = new FormFile(imageFile, 0, imageFile.Length, "Captura1", "Captura1.png")
            {
                Headers = new HeaderDictionary(),
                ContentType = "image/png"
            };
            return newFile;
        }
        #endregion

        #region Tests
        [TestMethod]
        public async Task Post_ValidEntity_200Code()
        {
            //Arrange
            var newActivity = new ActivityDTOForRegister
            {
                Name = "Activity name",
                Content = "Activity content",
                file = CreateImage(),
            };

            //Act
            var response = await activitiesController.Post(newActivity);
            var result = response as ObjectResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }

        [TestMethod]
        public async Task Post_NullEntity_400Code()
        {
            //Arrange
            var newActivity = new ActivityDTOForRegister();           

            //Act
            var response = await activitiesController.Post(newActivity);
            var result = response as ObjectResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
        }

        [TestMethod]
        public async Task Put_ValidEntity_200Code()
        {
            //Arrange
            var newActivity = new ActivitiesDtoForUpload
            {
                Name = "Activity name",
                Content = "Activity content",
                Image = CreateImage(),
            };

            //Act
            var response = await activitiesController.Put(1, newActivity);
            var result = response as ObjectResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }

        [TestMethod]
        public async Task Put_EntityNotFound_404Code()
        {
            //Arrange
            var newActivity = new ActivitiesDtoForUpload
            {
                Name = "Activity name",
                Content = "Activity content",
                Image = CreateImage(),
            };

            //Act
            var response = await activitiesController.Put((-1), newActivity);
            var result = response as ObjectResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(404, result.StatusCode);
        }
        #endregion
    }
}