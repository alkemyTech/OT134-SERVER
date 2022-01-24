using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OngProject.Entities
{
    public class Slides : EntityBase
    {
        [Display(Name = "UrlDeImagen")]
        [StringLength(255)]
        public string ImageUrl { get; set; }

        [Display(Name = "Texto")]
        [StringLength(2000)]
        public string Text { get; set; }

        [Display(Name = "Orden")]
        [StringLength(255)]
        public string order { get; set; }

        [Display(Name = "IdDeOrganizacion")]
        [ForeignKey("OrganizationId")]
        public int OrganizationId { get; set; }
    }
}
