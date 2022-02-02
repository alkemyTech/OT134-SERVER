using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using System.Threading.Tasks;


namespace OngProject.Core.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO> GetAll();
        User GetById();
        Task<UserDetailDto> Insert(UserRegisterDto dto);
        Task<UserDetailDto> LoginAsync(UserLoginDTO userLoginDto);
        void Update(User user);
        void Delete(User user);
    }
}