using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using System.Threading.Tasks;
using OngProject.Core.Models.Response;

namespace OngProject.Core.Interfaces
{
    public interface IUserService
    {

        Task<Result> GetAll();
        User GetById();
        Task<Result> Insert(UserRegisterDto dto);
        Task<Result> LoginAsync(UserLoginDTO userLoginDto);
        void Update(User user);
        Task<Result> Delete(int id);

    }
}