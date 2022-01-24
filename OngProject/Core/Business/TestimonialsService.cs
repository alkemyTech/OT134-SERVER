using OngProject.Core.Interfaces;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public void Delete(Testimonials testimonials)
        {
            throw new NotImplementedException();
        }
    }
}
