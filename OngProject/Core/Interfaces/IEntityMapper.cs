using OngProject.Core.Models.DTOs;
using OngProject.Entities;

namespace OngProject.Core.Interfaces
{
    public interface IEntityMapper
    {
        OrganizationDTO OrganizationToOrganizationDto(Organization organization);
        UserDTO UserToUserDto(User user);
        UserDetailDto UserToUserDetailDto(User user);
        User UserRegisterDtoToUser(UserRegisterDto dto);
        SlideDTO SlideToSlideDTO(Slides slides);
        ContactDTO ContactToContactDTO(Contacts contacts);
        Contacts ContactDTOToContact(ContactDTO contactsDto);
        CommentDTO CommentToCommentDTO(Comment comment);
        MemberDTO MemberToMemberDTO(Member member);
        CategoryDTO CategoryToCategoryDTO(Category category);
        Comment CommentDTOToComment(CommentDTO dto);
        New NewDtoForUploadtoNew(NewDtoForUpload newDTO);
        NewDtoForDisplay NewtoNewDtoForDisplay(New newvar);
        Activities ActivityDTOToActivity(ActivityDTO dto);
        Member MemberDTOToMember(MemberDTO memberDTO); 
        TestimonialDTO TestimonialToTestimonialDTO(Testimonials testimonial);
        Testimonials TestimonialDTOToTestimonial(TestimonialDTO testimonialDTO);
    }
}