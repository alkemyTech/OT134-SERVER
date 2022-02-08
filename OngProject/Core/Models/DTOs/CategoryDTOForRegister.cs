using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.Models.DTOs
{
    public class CategoryDTOForRegister
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public Microsoft.AspNetCore.Http.IFormFile Image { get; set; }
    }
}
