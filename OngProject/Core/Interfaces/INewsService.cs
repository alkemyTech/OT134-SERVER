using OngProject.Core.Models.DTOs;
using OngProject.Core.Models.Response;
using OngProject.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface INewsService
    {
        Task<ICollection<NewDtoForDisplay>> GetAll();
        Task<Result> GetById(int id);
        Task<Result> Insert(NewDtoForUpload newDTO);
        Task<Result> Update(int id, NewDtoForUpload newsDTO);
        Task<Result> Delete(int id);
    }
}
