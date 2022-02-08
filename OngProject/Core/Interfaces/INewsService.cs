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
        New GetById();
        Task<Result> Insert(NewDtoForUpload newDTO);
        Task<Result> Update(New news);
        Task<Result> Delete(New news);
    }
}
