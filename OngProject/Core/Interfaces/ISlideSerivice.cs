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
        void Insert(Slides slides);
        void Update(Slides slides);
        void Delete(Slides slides);
    }
}