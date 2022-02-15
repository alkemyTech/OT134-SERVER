using OngProject.Core.Models.DTOs;
using OngProject.Core.Models.Response;
using OngProject.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface ISlideSerivice
    {
        Task<ICollection<SlideDtoForDisplay>>GetAll();
        Task<Result> GetById(int id);
        Task<Result> Insert(SlideDtoForRegister slideDto);
        Task<Result> Update(int id, SlideDtoForUpdate dto);
        Task<ICollection<SlideDtoForDisplay>> GetAllByOrganization(int idOrganization);
        Task<Result> Delete(int id);
    }
}