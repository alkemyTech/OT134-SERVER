using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;

namespace OngProject.Core.Mapper
{
    public class EntityMapper : IEntityMapper
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

        public UserDetailDto UserToUserDetailDto(User user)
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
                RolId = dto.RolId
            };
        }

        public SlideDTO SlideToSlideDTO(Slides slides)
        {
            var slideDto = new SlideDTO
            {
                Order = slides.Order,
                Text = slides.Text,
                ImageUrl = slides.ImageUrl,
                OrganizationId = slides.OrganizationId
            };
            return slideDto;
        }

        public Slides SlideDTOToSlide(SlideDTO slideDto)
        {
            var slide = new Slides
            {
                Order = slideDto.Order,
                Text = slideDto.Text,
                ImageUrl = slideDto.ImageUrl,
                OrganizationId = slideDto.OrganizationId
            };
            return slide;
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
        public Contacts ContactDTOToContact(ContactDTO contactsDto)
        {
            var contacts = new Contacts
            {
                Email = contactsDto.Email,
                Message = contactsDto.Message,
                Name = contactsDto.Name,
                Phone = contactsDto.Phone
            };
            return contacts;
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

        public Comment CommentDTOToComment(CommentDTO dto)
        {
            var commetn = new Comment
            {
                Body = dto.Body,
                NewId = dto.NewId,
                UserId = dto.UserId
            };
            return commetn;
        }

        public ActivityDTOForDisplay ActivityForActivityDTODisplay(Activities dto)
        {
            var activityDisplay = new ActivityDTOForDisplay
            {
                 Name=dto.Name,
                 Content = dto.Content,
                 Image = dto.Image
            };
            return activityDisplay;
        }
        public Activities ActivityDTOForRegister(ActivityDTOForRegister dto)
        {
            var activityRegister = new Activities
            {
                Name = dto.Name,
                Content = dto.Content
            };
            return activityRegister;
        }
        public New NewDtoForUploadtoNew(NewDtoForUpload newDtoForUpload)
        {
            New newEntity = new()
            {
                Name = newDtoForUpload.Name,
                Content = newDtoForUpload.Content,
                CategoryId = newDtoForUpload.Category
            };
            return newEntity;
        }
        public NewDtoForDisplay NewtoNewDtoForDisplay(New newEntity)
        {
            NewDtoForDisplay newEntityForDisplay = new()
            {
                Name = newEntity.Name,
                Content = newEntity.Content,
                Image = newEntity.Image,
                Category = newEntity.CategoryId
            };
            return newEntityForDisplay;
        }
        public Member MemberDTOToMember(MemberDTO memberDTO)
        {
            var member = new Member
            {
                Name = memberDTO.Name,
                Description = memberDTO.Description,
                Image = memberDTO.File.FileName,
            };
            return member;
        }

        public Testimonials TestimonialDTOToTestimonial(TestimonialDTO testimonialDTO)
        {
            var testimonial = new Testimonials
            {
                Name = testimonialDTO.Name,
                Content = testimonialDTO.Content,
                Image = testimonialDTO.File.FileName,
            };
            return testimonial;
        }

        public TestimonialDTO TestimonialToTestimonialDTO(Testimonials testimonial)
        {
            var testimonialDTO = new TestimonialDTO
            {
                Name = testimonial.Name,
                Content = testimonial.Content,
            };
            return testimonialDTO;
        }
    }
}