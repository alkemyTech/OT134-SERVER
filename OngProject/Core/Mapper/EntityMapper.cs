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
        public SlideDTO SlideToSlideDTO(Slides slides) 
        {
            var slideDto = new SlideDTO
            {
                order = slides.order,
                ImageUrl = slides.ImageUrl
            };
            return slideDto;
        }
    }
}
