using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface IUserService
    {
        public Task<IEnumerable<UserDTO>> GetAll();
        public User GetById();
        public Task<UserDetailDto> Insert(UserRegisterDto dto);
        public void Update(User user);
        public void Delete(User user);
    }
}