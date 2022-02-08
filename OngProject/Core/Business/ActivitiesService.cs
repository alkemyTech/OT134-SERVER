using OngProject.Core.Helper;
using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
using OngProject.Core.Models.DTOs;
using OngProject.Core.Models.Response;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Core.Business
{
    public class ActivitiesService : IActivitiesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly EntityMapper _mapper;
        private readonly ImageService _imageService;
        public ActivitiesService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = new EntityMapper();
            _imageService = new ImageService(_unitOfWork);
        }

        public IEnumerable<Activities> GetAll()
        {
            throw new NotImplementedException();
        }

        public Activities GetById()
        {
            throw new NotImplementedException();
        }
        public void Update(Activities activities)
        {
            throw new NotImplementedException();
        }

        public void Delete(Activities activities)
        {
            throw new NotImplementedException();
        }

        public async Task<Result> Insert(ActivityDTOForRegister activities)
        {
            try
            {
                var activity = _mapper.ActivityDTOForRegister(activities);

                var resultName = await this._unitOfWork.ActivitiesRepository.FindByConditionAsync(x => x.Name == activities.Name);
                var resultContent = await this._unitOfWork.ActivitiesRepository.FindByConditionAsync(x => x.Content == activities.Content);
                if (resultName.Count > 0)
                {
                    return Result.FailureResult("El Nombre ya existe en el sistema, intente uno diferente al ingresado.");
                }
                else if (resultContent.Count > 0)
                {
                    return Result.FailureResult("El Contenido ya existe en el sistema, intente uno diferente al ingresado.");
                }
                else
                {
                    var aws = new S3AwsHelper();
                    var result = await _imageService.UploadFile($"{Guid.NewGuid()}_{activities.file.FileName}", activities.file);
                    
                    activity.LastModified = DateTime.Today;
                    activity.Image = result;

                    await _unitOfWork.ActivitiesRepository.Create(activity);
                    await _unitOfWork.SaveChangesAsync();


                    var obj = _mapper.ActivityForActivityDTODisplay(activity);
                    return Result<ActivityDTOForDisplay>.SuccessResult(obj);
                }
            }
            catch (Exception ex)
            {
                return Result.FailureResult($"Actividad no registrada: {ex.Message}");
            }
        }
    }
}
