using OngProject.Core.Models.DTOs;
using OngProject.Core.Models.Response;
using OngProject.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface IOrganizationsService
    {
        Task<IEnumerable<OrganizationDTOForDisplay>> GetAll();
        Organization GetById();
        Task<Result> Insert(OrganizationDTOForUpload organizationDTOForUpload);
        Task<Result> Update(int id, OrganizationDTOForUpload organizationDTOForUpload);
        void Delete(Organization organization);
    }
}