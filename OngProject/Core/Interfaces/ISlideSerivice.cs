using OngProject.Core.Models.DTOs;
using OngProject.Core.Models.Response;
using OngProject.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface ISlideSerivice
    {
        Task<ICollection<SlideDTO>>GetAll();
        Task<Result> GetById(int id);
        Task<Result> Insert(SlideDTO slideDto);
        void Update(Slides slides);
        Task<ICollection<SlideDTO>> GetAllByOrganization(int idOrganization);
        Task<Result> Delete(int id);
    }
}