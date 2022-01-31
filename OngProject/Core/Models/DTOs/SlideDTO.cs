using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace OngProject.Core.Models.DTOs
{
    public class SlideDTO
    {
        [Display(Name = "UrlDeImagen")]
        [StringLength(255)]
        public string ImageUrl { get; set; }

        [Display(Name = "Orden")]
        [StringLength(255)]
        public string order { get; set; }

    }
}
