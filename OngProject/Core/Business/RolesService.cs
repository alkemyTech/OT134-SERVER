using OngProject.Core.Interfaces;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;

namespace OngProject.Core.Business
{
    public class RolesService : IRolesService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RolesService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Delete(Rol rol)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Rol> GetAll()
        {
            throw new NotImplementedException();
        }

        public Rol GetById()
        {
            throw new NotImplementedException();
        }

        public void Insert(Rol rol)
        {
            throw new NotImplementedException();
        }

        public void Update(Rol rol)
        {
            throw new NotImplementedException();
        }
    }
}