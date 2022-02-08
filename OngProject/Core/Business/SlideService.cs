using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs;
using OngProject.Core.Models.Response;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Core.Business
{
    public class SlideService : ISlideSerivice
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntityMapper _mapper;

        public SlideService(IUnitOfWork unitOfWork, IEntityMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result> Delete(int id)
        {
            try
            {
                Slides slide = await _unitOfWork.SlideRepository.GetByIdAsync(id);

                if (slide != null && !slide.SoftDelete)
                {
                    slide.SoftDelete = true;
                    slide.LastModified = DateTime.Now;
                    _unitOfWork.SaveChanges();

                    return Result.SuccessResult();
                }
                else
                {
                    if (slide == null)
                        return Result.FailureResult("No se encontro ningun Slide con Id ingresado");
                    else
                        return Result.FailureResult("Slide con Id ingresado ha sido eliminado previamente");
                }
            }
            catch (Exception ex)
            {
                return Result.FailureResult(ex.Message);
            }
        }

        public async Task<ICollection<SlideDTO>> GetAll()
        {
            var response = await _unitOfWork.SlideRepository.FindByConditionAsync(x => x.SoftDelete == false);
            if (response.Count == 0)
            {
                return null;
            }
            else 
            {
                List<SlideDTO> dto = new ();
                foreach (var item in response)
                {
                    dto.Add(_mapper.SlideToSlideDTO(item));
                }
                return dto;
            }
        }

        public async Task<Result> GetById(int id)
        {
            try
            {
                Slides slide = await _unitOfWork.SlideRepository.GetByIdAsync(id);

                if (slide != null && !slide.SoftDelete)
                    return Result<Slides>.SuccessResult(slide);
                else
                {
                    if (slide == null)
                        return Result.FailureResult("No se encontro ningun Slide con Id ingresado");
                    else
                        return Result.FailureResult("Slide con Id ingresado ha sido eliminado previamente");
                }
            }
            catch(Exception ex)
            {
                return Result.FailureResult(ex.Message);
            }
        }

        public void Insert(Slides slides)
        {
            throw new System.NotImplementedException();
        }

        public void Update(Slides slides)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ICollection<SlideDTO>> GetAllByOrganization(int idOrganization)
        {
            var response = await _unitOfWork.SlideRepository.FindByConditionAsync(x => x.OrganizationId == idOrganization);
            if (response.Count == 0)
            {
                return null;
            }
            else
            {
                List<SlideDTO> dto = new();
                foreach (var item in response)
                {
                    dto.Add(_mapper.SlideToSlideDTO(item));
                }
                return dto;
            }
        }
    }
}
