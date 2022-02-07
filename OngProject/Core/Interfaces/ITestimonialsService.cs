using OngProject.Core.Models.Response;
using OngProject.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface ITestimonialsService
    {
        public IEnumerable<Testimonials> GetAll();
        public Testimonials GetById();
        public void Insert(Testimonials testimonials);
        public void Update(Testimonials testimonials);
        public Task<Result> Delete(int id);
    }
}
