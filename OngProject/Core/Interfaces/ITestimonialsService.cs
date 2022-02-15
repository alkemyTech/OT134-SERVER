using OngProject.Core.Models.DTOs;
using OngProject.Core.Models.PagedResourceParameters;
using OngProject.Core.Models.Response;
using OngProject.Entities;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface ITestimonialsService
    {
        Task<Result> GetAll(PaginationParams paginationParams);
        Testimonials GetById();
        Task<Result> Insert(TestimonialDTO testimonialDTO);
        Task<Result> Update(int id,TestimonialDTO testimonialDTO);
        Task<Result> Delete(int id);
    }
}