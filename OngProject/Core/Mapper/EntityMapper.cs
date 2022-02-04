using OngProject.Core.Models.DTOs;
using OngProject.Entities;

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
                Address = organization.Address,
                FacebookUrl = organization.FacebookUrl,
                InstagramUrl = organization.InstagramUrl,
                LinkedinUrl = organization.LinkedinUrl
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

        public UserDetailDto UseToUserDetailDto(User user)
        {
            return new UserDetailDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,                
                Photo = user.Photo,
                RolId = user.RolId
            };
        }

        public User UserRegisterDtoToUser(UserRegisterDto dto)
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

        public SlideDTO SlideToSlideDTO(Slides slides) 
        {
            var slideDto = new SlideDTO
            {
                order = slides.order,
                ImageUrl = slides.ImageUrl
            };
            return slideDto;

        }
        public ContactDTO ContactToContactDTO(Contacts contacts)
        {
            var contactDto = new ContactDTO
            {
                Email = contacts.Email,
                Message = contacts.Message,
                Name = contacts.Name,
                Phone = contacts.Phone
            };
            return contactDto;
        }
        public CommentDTO CommentToCommentDTO(Comment comment)
        {
            var commentDTO = new CommentDTO
            {
                Body = comment.Body
            };
            return commentDTO;
        }
        public MemberDTO MemberToMemberDTO(Member member)
        {
            var memberDTO = new MemberDTO
            {
                Name = member.Name,
                Description = member.Description,
            };
            return memberDTO;
        }

        public CategoryDTO CategoryToCategoryDTO(Category category)
        {
            var categoryDto = new CategoryDTO
            {
                Name = category.Name,
            };

            return categoryDto;
        }
        public Comment CommentDTOToComment(CommentDTO commentDTO)
        {
            var comment = new Comment
            {
                Body = commentDTO.Body,
                UserId = commentDTO.IdUser,
                NewId = commentDTO.NewId
            };
            return comment;
        }
    }
}