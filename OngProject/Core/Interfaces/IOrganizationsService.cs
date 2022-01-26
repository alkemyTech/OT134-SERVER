using OngProject.Entities;
using System.Collections.Generic;

namespace OngProject.Core.Interfaces
{
    public interface IOrganizationsService
    {
        public IEnumerable<Organization> GetAll();
        public Organization GetById();
        public void Insert(Organization organization);
        public void Update(Organization organization);
        public void Delete(Organization organization);
    }
}