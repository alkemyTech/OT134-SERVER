using OngProject.Core.Interfaces;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;

namespace OngProject.Core.Business
{
    public class OrganizacionService : IOrganizationsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrganizacionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Delete(Organizacion organization)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Organizacion> GetAll()
        {
            throw new NotImplementedException();
        }

        public Activities GetById()
        {
            throw new NotImplementedException();
        }

        public void Insert(Organizacion organization)
        {
            throw new NotImplementedException();
        }

        public void Update(Organizacion organization)
        {
            throw new NotImplementedException();
        }
    }
}