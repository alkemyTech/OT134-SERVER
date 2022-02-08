using OngProject.Core.Interfaces;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System;
using System.Threading.Tasks;
using System.Linq;
using OngProject.Core.Models.DTOs;
using OngProject.Core.Models.Response;
using System.Collections.Generic;

namespace OngProject.Core.Business
{
    public class OrganizationService : IOrganizationsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntityMapper _mapper;
        private readonly ISlideSerivice _slideSerivice;

        public OrganizationService(IUnitOfWork unitOfWork, IEntityMapper mapper, ISlideSerivice slideSerivice)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _slideSerivice = slideSerivice;
        }

        public void Delete(Organization organization)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<OrganizationDTO>> GetAll()
        {
            var response = await _unitOfWork.OrganizationRepository.FindByConditionAsync(x => !x.SoftDelete);

            List<OrganizationDTO> organizationDto = new();

            if (response.Count > 0)
            {
                foreach (var entity in response)
                {
                    var orgDto = _mapper.OrganizationToOrganizationDto(entity);

                    orgDto.Slides = await _slideSerivice.GetAllByOrganization(entity.Id);

                    organizationDto.Add(orgDto);
                }

                return organizationDto;
            }
            else
                return null;
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