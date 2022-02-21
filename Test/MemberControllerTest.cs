using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OngProject.Controllers;
using OngProject.Core.Business;
using OngProject.Core.Models.DTOs;
using OngProject.Core.Models.Paged;
using OngProject.Core.Models.PagedResourceParameters;
using OngProject.Core.Models.Response;
using System.IO;
using System.Threading.Tasks;
using Test.Helper;

namespace Test
{
    [TestClass]
    public class MemberControllerTest
    {

        [TestMethod]
        public async Task DeleteMemeberSuccessfullyTest()
        {
            // Arrange            
            var memberServie = new MemberService(ContextHelper.UnitOfWork, ContextHelper.EntityMapper, ContextHelper.httpContext, ContextHelper.ImageService);
            var mebmerController = new MembersController(memberServie);
            var member = await ContextHelper.UnitOfWork.MembersRepository.GetByIdAsync(3);

            // Act
            var response = await mebmerController.Delete(member.Id);
            var result = response as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsTrue(member.SoftDelete);
        }
        [TestMethod]
        public async Task DeleteMemeberNotFoundTest()
        {
            // Arrange            
            var memberServie = new MemberService(ContextHelper.UnitOfWork, ContextHelper.EntityMapper, ContextHelper.httpContext, ContextHelper.ImageService);
            var mebmerController = new MembersController(memberServie);

            // Act
            var response = await mebmerController.Delete(100);
            var result = response as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(404, result.StatusCode);
        }
        [TestMethod]
        public async Task DeleteMemeberAlreadyDeletedmemberTest()
        {
            // Arrange            
            var memberServie = new MemberService(ContextHelper.UnitOfWork, ContextHelper.EntityMapper, ContextHelper.httpContext, ContextHelper.ImageService);
            var mebmerController = new MembersController(memberServie);
            var member = await ContextHelper.UnitOfWork.MembersRepository.GetByIdAsync(1);

            // Act
            var response = await mebmerController.Delete(member.Id);
            var result = response as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(404, result.StatusCode);
            Assert.IsTrue(member.SoftDelete);
        }
        [TestMethod]
        public async Task GetAllMembersSuccessfullyTest()
        {
            // Arrange
            HostString myUri = new HostString("www.api.com:8080");
            var MockMemberHttpContex = new Mock<HttpContext>();
            MockMemberHttpContex.Setup(x => x.Request.Scheme).Returns("https");
            MockMemberHttpContex.Setup(x => x.Request.Host).Returns(myUri);
            MockMemberHttpContex.Setup(x => x.Request.Path).Returns("/Members");

            ContextHelper.httpContext.HttpContext = MockMemberHttpContex.Object;

            var memberServie = new MemberService(ContextHelper.UnitOfWork, ContextHelper.EntityMapper, ContextHelper.httpContext, ContextHelper.ImageService);
            var memberController = new MembersController(memberServie);

            // Act
            PaginationParams paginationParams = new PaginationParams();
            paginationParams.PageNumber = 1;
            paginationParams.PageSize = 5;
            var response = await memberController.GetAllMembers(paginationParams);
            var objectResult = response as ObjectResult;
            var result = objectResult.Value as Result<PagedResponse<MemberDTODisplay>>;

            // Assert
            Assert.IsNotNull(objectResult);
            Assert.AreEqual(200, objectResult.StatusCode);
            Assert.IsTrue(result.Success);
        }
        [TestMethod]
        public async Task AddMemeberSuccessfullyTest()
        {
            // Arrange     
            var Image = CreateImage();

            var memberDTO = new MemberDTORegister()
            {
                Name = "nombre de prueba",
                Description = "Descripcion de prueba",
                File = Image
            };
            var mockImageService = new Mock<ImageService>(ContextHelper.UnitOfWork);
            var link = "https://www.netmentor.es/entrada/mock-unit-test-csharp";
            mockImageService.Setup(x => x.UploadFile(It.IsAny<string>(), It.IsAny<IFormFile>())).ReturnsAsync(link);
            var memberServie = new MemberService(ContextHelper.UnitOfWork, ContextHelper.EntityMapper, ContextHelper.httpContext, mockImageService.Object);
            var mebmerController = new MembersController(memberServie);

            // Act
            var response = await mebmerController.Post(memberDTO);
            var result = response as ObjectResult;
            var resultDTO = result.Value as Result<MemberDTODisplay>;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(resultDTO);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsInstanceOfType(resultDTO.Data, typeof(MemberDTODisplay));
        }
        [TestMethod]
        public async Task AddingAMemberWithNullNameTest()
        {
            // Arrange     
            var Image = CreateImage();

            var memberDTO = new MemberDTORegister()
            {
                Name = null,
                Description = "Descripcion de prueba",
                File = Image
            };
            var mockImageService = new Mock<ImageService>(ContextHelper.UnitOfWork);
            var link = "https://www.netmentor.es/entrada/mock-unit-test-csharp";
            mockImageService.Setup(x => x.UploadFile(It.IsAny<string>(), It.IsAny<IFormFile>())).ReturnsAsync(link);
            var memberServie = new MemberService(ContextHelper.UnitOfWork, ContextHelper.EntityMapper, ContextHelper.httpContext, mockImageService.Object);
            var mebmerController = new MembersController(memberServie);

            // Act
            var response = await mebmerController.Post(memberDTO);
            var result = response as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
        }
        [TestMethod]
        public async Task AddingAMemberWithNullImageTest()
        {
            // Arrange     
            var memberDTO = new MemberDTORegister()
            {
                Name = "nombre de prueba",
                Description = "Descripcion de prueba",
                File = null
            };
            var mockImageService = new Mock<ImageService>(ContextHelper.UnitOfWork);
            var link = "https://www.netmentor.es/entrada/mock-unit-test-csharp";
            mockImageService.Setup(x => x.UploadFile(It.IsAny<string>(), It.IsAny<IFormFile>())).ReturnsAsync(link);
            var memberServie = new MemberService(ContextHelper.UnitOfWork, ContextHelper.EntityMapper, ContextHelper.httpContext, mockImageService.Object);
            var mebmerController = new MembersController(memberServie);

            // Act
            var response = await mebmerController.Post(memberDTO);
            var result = response as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
        }
        [TestMethod]
        public async Task AddingAnExistingMember()
        {
            // Arrange
            var Image = CreateImage();
            var memberDTO = new MemberDTORegister()
            {
                Name = "Name 1",
                Description = "Descripcion de prueba",
                File = Image
            };
            var mockImageService = new Mock<ImageService>(ContextHelper.UnitOfWork);
            var link = "https://www.netmentor.es/entrada/mock-unit-test-csharp";
            mockImageService.Setup(x => x.UploadFile(It.IsAny<string>(), It.IsAny<IFormFile>())).ReturnsAsync(link);
            var memberServie = new MemberService(ContextHelper.UnitOfWork, ContextHelper.EntityMapper, ContextHelper.httpContext, mockImageService.Object);
            var mebmerController = new MembersController(memberServie);

            // Act
            var response = await mebmerController.Post(memberDTO);
            var result = response as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
        }
        [TestMethod]
        public async Task UpgradingASuccessulMemberTest()
        {
            // Arrange
            var Image = CreateImage();
            int IdMember = 1;
            var memberDTO = new MembersDtoForUpload()
            {
                Name = "Name 1",
                Description = "Descripcion de prueba",
                Image = Image,
                FacebookUrl = "FacebookContact",
                InstagramUrl = "InstagramContact",
                LinkedinUrl = "LinkedinContact"
            };
            var mockImageService = new Mock<ImageService>(ContextHelper.UnitOfWork);
            var link = "https://www.netmentor.es/entrada/mock-unit-test-csharp";
            mockImageService.Setup(x => x.UploadFile(It.IsAny<string>(), It.IsAny<IFormFile>())).ReturnsAsync(link);
            mockImageService.Setup(x => x.AwsDeleteFile(It.IsAny<string>()));
            var memberServie = new MemberService(ContextHelper.UnitOfWork, ContextHelper.EntityMapper, ContextHelper.httpContext, mockImageService.Object);
            var mebmerController = new MembersController(memberServie);

            // Act
            var response = await mebmerController.Put(IdMember, memberDTO);
            var result = response as ObjectResult;
            var resultDTO = result.Value as Result<MemberDTODisplay>;
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsInstanceOfType(resultDTO.Data, typeof(MemberDTODisplay));
        }
        [TestMethod]
        public async Task TryingToUpdatANonexistentMemberTest()
        {
            // Arrange
            var Image = CreateImage();
            int IdMember = 100;
            var memberDTO = new MembersDtoForUpload()
            {
                Name = "Name 1",
                Description = "Descripcion de prueba",
                Image = Image,
                FacebookUrl = "FacebookContact",
                InstagramUrl = "InstagramContact",
                LinkedinUrl = "LinkedinContact"
            };
            var mockImageService = new Mock<ImageService>(ContextHelper.UnitOfWork);
            var link = "https://www.netmentor.es/entrada/mock-unit-test-csharp";
            mockImageService.Setup(x => x.UploadFile(It.IsAny<string>(), It.IsAny<IFormFile>())).ReturnsAsync(link);
            mockImageService.Setup(x => x.AwsDeleteFile(It.IsAny<string>()));
            var memberServie = new MemberService(ContextHelper.UnitOfWork, ContextHelper.EntityMapper, ContextHelper.httpContext, mockImageService.Object);
            var mebmerController = new MembersController(memberServie);

            // Act
            var response = await mebmerController.Put(IdMember, memberDTO);
            var result = response as ObjectResult;
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(404, result.StatusCode);
        }
        //metodo para seleccionar la imagen por defecto
        public IFormFile CreateImage()
        {
            //cambiar la ubicacion de la imagen para que detecte el archivo si se ejecuta desde otra pc
            var stream = File.OpenRead(@"C:\Users\Emiliano\Desktop\ONG\Test\Image\Captura1.png");
            var image = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name))
            {
                Headers = new HeaderDictionary(),
                ContentType = "image/png"
            };
            return image;
        }
    }
}