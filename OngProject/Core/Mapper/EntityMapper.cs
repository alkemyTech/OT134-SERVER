using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Mapper
{
    public class EntityMapper
    {
        public OrganizationDTO OrganizationToOrganizationDto(Organization organization)
        {
            var organizationDto = new OrganizationDTO
            {
                Name = organization.Name,
                Image = organization.Image,
                Phone = organization.Phone,
                Address = organization.Address
            };
            return organizationDto;
        }

        public UserDTO UserToUserDto(User user)
        {
            var userDto = new UserDTO
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };

            return userDto;
        }
    }
}
