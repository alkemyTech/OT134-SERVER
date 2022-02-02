using OngProject.Core.Models.DTOs;
using OngProject.Entities;

using System.Threading.Tasks;


using System.Collections.Generic;
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

        public Task<IEnumerable<UserDTO>> GetAll();
        public User GetById();
        public Task<UserDetailDto> Insert(UserRegisterDto dto);
        public void Update(User user);
        public void Delete(User user);

    }
}