using OngProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace OngProject.Core.Interfaces
{
    public interface ICategoryService
    {
        public IEnumerable<Category> GetAll();
        public Category GetById();
        public void Insert(Category category);
        public void Update(Category category);
        public void Delete(Category category);
    }
}
