using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OngProject.Entities;
using OngProject.Core.Models.DTOs;

namespace OngProject.Core.Mapper
{
    public class UserMapper
    {
        public static User mapUserRegisterDtoToUser(UserRegisterDto dto)
        {
            return new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Password = dto.Password,
                Photo = dto.Photo,
                RolId = dto.RolId
            };
        }
    }
}
