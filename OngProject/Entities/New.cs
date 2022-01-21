using System.ComponentModel.DataAnnotations;

namespace OngProject.Entities
{
    public class New: EntityBase
    {
        [Required(ErrorMessage = "The Name Is Required")]
        [StringLength(maximumLength: 255, ErrorMessage = "The Name Is Too Long")]
        public string Name { get; set; }
        [Required(ErrorMessage = "The Content Is Required")]
        [StringLength(maximumLength: 65535, ErrorMessage = "The Content Is Too Long")]
        public  string Content { get; set; }
        [Required(ErrorMessage = "The Image Is Required")]
        [StringLength(maximumLength: 255, ErrorMessage = "The Name Of The Image Is Too Long")]
        public string Image { get; set; }
    }
}
