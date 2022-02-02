using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Repositories.Interfaces;
using System.Threading.Tasks;

namespace OngProject.Core.Business
{
    public class ImageService : IImageService
    {
        private readonly IS3AwsHelper _s3AwsHelper;
        private readonly IUnitOfWork _unitOfWork;
        public ImageService(IS3AwsHelper s3AwsHelper, IUnitOfWork unitOfWork)
        {
            this._s3AwsHelper = s3AwsHelper;
            _unitOfWork = unitOfWork;
        }
        public async Task<string> UploadFile(string key, IFormFile file)
        {
            var result = await _s3AwsHelper.AwsUploadFile(key, file);
            return result.Url.ToString();
        }
        public async Task<FileStreamResult> GetFile([FromQuery] string imageName)
        {
            var result = await _s3AwsHelper.AwsGetFile(imageName);
            return result;
        }
        public async Task<string> AwsDeleteFile([FromQuery] string imageName)
        {
            var result = await _s3AwsHelper.AwsDeleteFile(imageName);
            return result.Message;
        }
    }
}