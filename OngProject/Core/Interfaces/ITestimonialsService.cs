using OngProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface ITestimonialsService
    {
        public IEnumerable<Testimonials> GetAll();
        public Testimonials GetById();
        public void Insert(Testimonials testimonials);
        public void Update(Testimonials testimonials);
        public void Delete(Testimonials testimonials);
    }
}
