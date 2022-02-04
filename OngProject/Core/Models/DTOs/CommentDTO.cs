using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.Models.DTOs
{
    public class CommentDTO
    {
        [Required(ErrorMessage = "Es necesario seleccionar una noticia")]
        public int NewId { get; set; }
        [Required(ErrorMessage = "Es necesario un Usuario")]
        public int UserId { get; set; }
        [Required(ErrorMessage = "Body es requerido.")]
        [StringLength(maximumLength: 65535, ErrorMessage = "Body es demasiado largo.")]
        public string Body { get; set; }
    }
}