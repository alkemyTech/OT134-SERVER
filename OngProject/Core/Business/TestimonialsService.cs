using OngProject.Core.Interfaces;
using OngProject.Core.Models.Response;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Core.Business
{
    public class TestimonialsService : ITestimonialsService
    {
        private readonly IUnitOfWork _unitOfWork;
        public TestimonialsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Testimonials> GetAll()
        {
            throw new NotImplementedException();
        }

        public Testimonials GetById()
        {
            throw new NotImplementedException();
        }

        public void Insert(Testimonials testimonials)
        {
            throw new NotImplementedException();
        }

        public void Update(Testimonials testimonials)
        {
            throw new NotImplementedException();
        }

        public async Task<Result> Delete(int id)
        {
            try
            {
                var testimonial = await _unitOfWork.TestimonialsRepository.GetByIdAsync(id);
                if (testimonial is not null)
                {
                    if (testimonial.SoftDelete)
                        return Result.FailureResult("El testimonio ya se encuentra eliminado del sistema");
                    
                    testimonial.SoftDelete = true;
                    testimonial.LastModified = DateTime.Now;
                    await this._unitOfWork.SaveChangesAsync();

                    return Result<Testimonials>.SuccessResult(testimonial);
                }

                return Result.FailureResult("No existe un testimonio con ese Id");
            }
            catch (Exception ex)
            {
                return Result.FailureResult($"Error al eliminar el testimonio: {ex.Message}");
            }
        }
    }
}
