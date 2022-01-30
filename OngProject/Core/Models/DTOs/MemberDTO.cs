using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.Models.DTOs
{
    public class MemberDTO
    {
        [Required(ErrorMessage = "The Name Is Required")]
        [StringLength(maximumLength: 255, ErrorMessage = "The Name Is Too Long")]
        public string Name { get; set; }

        [StringLength(maximumLength: 255, ErrorMessage = "The Description Is Too Long")]
        public string Description { get; set; }
    }
}