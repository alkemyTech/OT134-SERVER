using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface ISlideSerivice
    {
        public Task<SlideDTO>GetAll();
        public Slides GetById();
        public void Insert(Slides slides);
        public void Update(Slides slides);
        public void Delete(Slides slides);
    }
}