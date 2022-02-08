using OngProject.Core.Models.DTOs;
using OngProject.Core.Models.Response;
using OngProject.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace OngProject.Core.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDtoForDisplay>> GetAll();
        Category GetById();
        Task<Result> Insert(CategoryDTOForRegister categoryDTO);
        void Update(Category category);
        Task<Result> Delete(int id);
    }
}
