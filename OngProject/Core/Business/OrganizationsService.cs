using OngProject.Core.Interfaces;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace OngProject.Core.Business
{
    public class OrganizationService : IOrganizationsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrganizationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Delete(Organization organization)
        {
            throw new NotImplementedException();
        }

        public async Task<Organization> GetAll()
        {
            return await _unitOfWork.OrganizationRepository.FindAll().FirstOrDefaultAsync();
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