using OngProject.Core.Interfaces;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System;
using System.Threading.Tasks;
using System.Linq;
using OngProject.Core.Models.DTOs;

namespace OngProject.Core.Business
{
    public class OrganizationService : IOrganizationsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntityMapper _mapper;

        public OrganizationService(IUnitOfWork unitOfWork, IEntityMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void Delete(Organization organization)
        {
            throw new NotImplementedException();
        }

        public async Task<OrganizationDTO> GetAll()
        {
            var response = await _unitOfWork.OrganizationRepository.FindAllAsync();
            return _mapper.OrganizationToOrganizationDto(response.FirstOrDefault());
        }

        public Organization GetById()
        {
            throw new NotImplementedException();
        }

        public void Insert(Organization organization)
        {
            throw new NotImplementedException();
        }

        public void Update(Organization organization)
        {
            throw new NotImplementedException();
        }
    }
}