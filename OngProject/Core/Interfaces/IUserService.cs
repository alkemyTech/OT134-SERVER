using OngProject.Core.Models.DTOs;
using OngProject.Entities;

using System.Threading.Tasks;


using System.Collections.Generic;



namespace OngProject.Core.Interfaces
{
    public interface IUserService
    {

        Task<IEnumerable<UserDTO>> GetAll();
        User GetById();
        Task<UserDetailDto> Insert(UserRegisterDto dto);
        Task<string> LoginAsync(UserLoginDTO userLoginDto);
        void Update(User user);
        void Delete(User user);

    }
}