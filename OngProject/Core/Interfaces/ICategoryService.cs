using OngProject.Core.Models.DTOs;
using OngProject.Core.Models.Response;
using OngProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace OngProject.Core.Interfaces
{
    public interface ICategoryService
    {
        public Task<IEnumerable<CategoryDTO>> GetAll();
        public Task<Result> GetById(int id);
        public Task<Result> Insert(CategoryDTO categoryDTO);
        public void Update(Category category);
        Task<Result> Delete(int id);
    }
}
