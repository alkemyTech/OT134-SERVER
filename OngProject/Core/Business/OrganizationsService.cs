using OngProject.Core.Interfaces;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;

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

        public IEnumerable<Organization> GetAll()
        {
            throw new NotImplementedException();
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