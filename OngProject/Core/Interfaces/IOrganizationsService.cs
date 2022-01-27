using OngProject.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface IOrganizationsService
    {
        public Task <Organization> GetAll();
        public Organization GetById();
        public void Insert(Organization organization);
        public void Update(Organization organization);
        public void Delete(Organization organization);
    }
}