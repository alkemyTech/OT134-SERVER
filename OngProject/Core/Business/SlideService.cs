using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
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

        public async Task<SlideDTO> GetAll()
        {
            var response = await _unitOfWork.SlideRepository.FindAllAsync();
            return _mapper.SlideToSlideDTO(response.FirstOrDefault());
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
