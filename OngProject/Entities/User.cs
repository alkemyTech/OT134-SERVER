using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OngProject.Entities
{
    public class User
    {
        [Required(ErrorMessage ="The First Name Is Required")]
        [MaxLength(255, ErrorMessage = "The First Name Is Too Long")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "The Last Name Is Required")]
        [MaxLength(255, ErrorMessage = "The Last Name Is Too Long")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "The Email Is Required")]
        [MaxLength(320, ErrorMessage = "The Email Is Too Long")]
        public string Email { get; set; }
        [Required(ErrorMessage = "The Password Is Required")]
        [MaxLength(20, ErrorMessage = "The Password Is Too Long")]
        public string Password { get; set; }
        [Required(ErrorMessage = "The Photo Name Is Required")]
        [MaxLength(255, ErrorMessage = "The Photo Name Is Too Long")]
        public string Photo { get; set; }
        [ForeignKey(nameof(Role))]
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}