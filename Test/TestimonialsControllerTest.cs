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
using System.ComponentModel.DataAnnotations;
using OngProject.Core.Models.PagedResourceParameters;
using OngProject.Core.Models.Paged;
using System;
using OngProject.Entities;

namespace Test
{
    [TestClass]
    public class TestimonialsControllerTest
    {
        [ClassInitialize()]
        public static void Setup(TestContext testContext)
        {
            ContextHelper.MakeDbContext();
        }

        [TestMethod]
        public async Task GetAllTestimonialsSuccessfullyTest()
        {
            // Arrange

            HostString myUri = new HostString("www.api.com:8080");
            var MockMemberHttpContex = new Mock<HttpContext>();
            MockMemberHttpContex.Setup(x => x.Request.Scheme).Returns("https");
            MockMemberHttpContex.Setup(x => x.Request.Host).Returns(myUri);
            MockMemberHttpContex.Setup(x => x.Request.Path).Returns("/testimonials");

            ContextHelper.httpContext.HttpContext = MockMemberHttpContex.Object;
            ContextHelper.MakeDbContext();
            var testimonialsService = new TestimonialsService(ContextHelper.UnitOfWork, 
                ContextHelper.Config, 
                ContextHelper.EntityMapper,
                ContextHelper.ImageService, 
                ContextHelper.httpContext);
            var testimonialsController = new TestimonialsController(testimonialsService);

            // Act
            PaginationParams paginationParams = new PaginationParams();
            paginationParams.PageNumber = 1;
            paginationParams.PageSize = 5;
            var response = await testimonialsController.GetAll(paginationParams);
            var objectResult = response as ObjectResult;
            var result = objectResult.Value as Result<PagedResponse<Testimonials>>;

            // Assert
            Assert.IsNotNull(objectResult);
            Assert.AreEqual(200, objectResult.StatusCode);
            Assert.IsTrue(result.Success);
        }
        [TestMethod]
        public async Task GetAllTestimonialsFailureThereAreNoTestimonialsTest()
        {
            // Arrange

            ContextHelper.MakeDbContext(false); // reinicializar DB sin datos
            var testimonialsService = new TestimonialsService(ContextHelper.UnitOfWork,
                ContextHelper.Config,
                ContextHelper.EntityMapper,
                ContextHelper.ImageService,
                ContextHelper.httpContext);
            var testimonialsController = new TestimonialsController(testimonialsService);

            // Act
            PaginationParams paginationParams = new PaginationParams();
            paginationParams.PageNumber = 1;
            paginationParams.PageSize = 5;
            var response = await testimonialsController.GetAll(paginationParams);
            var objectResult = response as ObjectResult;

            // Assert
            Assert.IsNotNull(objectResult);
            Assert.AreEqual(404, objectResult.StatusCode);
            ContextHelper.MakeDbContext();
        }

        [TestMethod]
        public async Task AddTestimonialsSuccessfullyTest()
        {
            // Arrange     
            var Image = CreateImage();

            var testimonialEntity = new TestimonialDTO()
            {
                Name = "test name",
                Content = "test content",
                File = Image,
            };
            var mockImageService = new Mock<ImageService>(ContextHelper.UnitOfWork);
            var link = "https://www.netmentor.es/entrada/mock-unit-test-csharp";
            mockImageService.Setup(x => x.UploadFile(It.IsAny<string>(), It.IsAny<IFormFile>())).ReturnsAsync(link);
            var testimonialsService = new TestimonialsService(
                ContextHelper.UnitOfWork, 
                ContextHelper.Config, 
                ContextHelper.EntityMapper, 
                mockImageService.Object, 
                ContextHelper.httpContext);
            var testimonialController = new TestimonialsController(testimonialsService);

            // Act
            var response = await testimonialController.Post(testimonialEntity);
            var result = response as ObjectResult;
            var resultDTO = result.Value as Result<TestimonialDTODisplay>;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(resultDTO);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsInstanceOfType(resultDTO.Data, typeof(TestimonialDTODisplay));
        }
        [TestMethod]
        public async Task UpdateTestimonialSuccessfullyTest()
        {
            // Arrange
            var Image = CreateImage();
            int IdTestimonial = 1;
            var testimonialEntity = new TestimonialDTO()
            {
                Name = "test name",
                Content = "test content",
                File = Image,
            };
            var mockImageService = new Mock<ImageService>(ContextHelper.UnitOfWork);
            var link = "https://www.netmentor.es/entrada/mock-unit-test-csharp";
            mockImageService.Setup(x => x.UploadFile(It.IsAny<string>(), It.IsAny<IFormFile>())).ReturnsAsync(link);
            mockImageService.Setup(x => x.AwsDeleteFile(It.IsAny<string>()));
            var testimonialsService = new TestimonialsService(ContextHelper.UnitOfWork,
                ContextHelper.Config,
                ContextHelper.EntityMapper,
                mockImageService.Object,
                ContextHelper.httpContext);
            var testimonialController = new TestimonialsController(testimonialsService);

            // Act
            var response = await testimonialController.Put(IdTestimonial, testimonialEntity);
            var result = response as ObjectResult;
            var resultDTO = result.Value as Result<TestimonialDTODisplay>;
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsInstanceOfType(resultDTO.Data, typeof(TestimonialDTODisplay));
        }

        [TestMethod]
        public async Task UpdateTestimonialsFailureNonExistingTestimonial()
        {
            // Arrange
            var Image = CreateImage();
            int IdTestimonial = 100;
            var testimonialEntity = new TestimonialDTO()
            {
                Name = "test name",
                Content = "test content",
                File = Image,
            };
            var mockImageService = new Mock<ImageService>(ContextHelper.UnitOfWork);
            var link = "https://www.netmentor.es/entrada/mock-unit-test-csharp";
            mockImageService.Setup(x => x.UploadFile(It.IsAny<string>(), It.IsAny<IFormFile>())).ReturnsAsync(link);
            mockImageService.Setup(x => x.AwsDeleteFile(It.IsAny<string>()));
            var testimonialsService = new TestimonialsService(ContextHelper.UnitOfWork,
                ContextHelper.Config,
                ContextHelper.EntityMapper,
                mockImageService.Object,
                ContextHelper.httpContext);
            var testimonialController = new TestimonialsController(testimonialsService);

            // Act
            var response = await testimonialController.Put(IdTestimonial, testimonialEntity);
            var result = response as ObjectResult;
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(404, result.StatusCode);
        }

        [TestMethod]
        public async Task DeleteTestimonialSuccessfullyTest()
        {
            // Arrange
            var testimonialsService = new TestimonialsService(ContextHelper.UnitOfWork,
                ContextHelper.Config,
                ContextHelper.EntityMapper,
                ContextHelper.ImageService,
                ContextHelper.httpContext);
            var testimonialsController = new TestimonialsController(testimonialsService);
            var testimonialEntity = await ContextHelper.UnitOfWork.TestimonialsRepository.GetByIdAsync(3);

            // Act
            var response = await testimonialsController.Delete(testimonialEntity.Id);
            var result = response as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsTrue(testimonialEntity.SoftDelete);
        }
        [TestMethod]
        public async Task DeleteTestimonialsNotFoundTest()
        {
            // Arrange            
            var testimonialsService = new TestimonialsService(ContextHelper.UnitOfWork,
                ContextHelper.Config,
                ContextHelper.EntityMapper,
                ContextHelper.ImageService,
                ContextHelper.httpContext);
            var testimonialsController = new TestimonialsController(testimonialsService);

            // Act
            var response = await testimonialsController.Delete(100);
            var result = response as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(404, result.StatusCode);
        }

        //metodo para seleccionar la imagen por defecto
        public IFormFile CreateImage()
        {
            //cambiar la ubicacion de la imagen para que detecte el archivo si se ejecuta desde otra pc
            var stream = File.OpenRead(@"C:\Users\Jose Luis\Pictures\DotNet.png");
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