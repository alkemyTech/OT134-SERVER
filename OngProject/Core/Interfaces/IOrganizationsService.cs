using OngProject.Entities;
using System.Collections.Generic;

namespace OngProject.Core.Interfaces
{
    public interface IOrganizationsService
    {
        public IEnumerable<Organizacion> GetAll();
        public Activities GetById();
        public void Insert(Organizacion organization);
        public void Update(Organizacion organization);
        public void Delete(Organizacion organization);
    }
}