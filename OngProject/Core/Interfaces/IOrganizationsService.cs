using OngProject.Core.Models.DTOs;
using OngProject.Core.Models.Response;
using OngProject.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface IOrganizationsService
    {
        Task<IEnumerable<OrganizationDTO>>GetAll();
        Organization GetById();
        void Insert(Organization organization);
        void Update(Organization organization);
        void Delete(Organization organization);
    }
}