using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Business
{
    public class SlideService : ISlideSerivice
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly EntityMapper _mapper;

        public SlideService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = new EntityMapper();
        }

        public void Delete(Slides slides)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ICollection<SlideDTO>> GetAll()
        {
            var response = await _unitOfWork.SlideRepository.FindAllAsync();
            if (response.Count == 0)
            {
                return null;
            }
            else 
            {
                List<SlideDTO> dto = new List<SlideDTO>();
                foreach (var item in response)
                {
                    dto.Add(_mapper.SlideToSlideDTO(item));
                }
                return dto;
            }
        }

        public Slides GetById()
        {
            throw new System.NotImplementedException();
        }

        public void Insert(Slides slides)
        {
            throw new System.NotImplementedException();
        }

        public void Update(Slides slides)
        {
            throw new System.NotImplementedException();
        }
    }
}
