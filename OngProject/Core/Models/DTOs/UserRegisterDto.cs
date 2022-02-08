using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace OngProject.Core.Models.DTOs
{
    public class UserRegisterDto
    {
        [Required(ErrorMessage = "The First Name Is Required")]
        [StringLength(maximumLength: 255, ErrorMessage = "The First Name Is Too Long")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "The Last Name Is Required")]
        [StringLength(maximumLength: 255, ErrorMessage = "The Last Name Is Too Long")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "The Email Is Required")]
        [StringLength(maximumLength: 320, ErrorMessage = "The Email Is Too Long")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter correct email")]        
        public string Email { get; set; }

        [Required(ErrorMessage = "The Password Is Required")]
        [StringLength(maximumLength: 255, ErrorMessage = "The Password Is Too Long")]
        public string Password { get; set; }

        [Required(ErrorMessage = "The Image is required")]        
        public IFormFile Photo { get; set; }

        public int RolId { get; set; }
    }
}
