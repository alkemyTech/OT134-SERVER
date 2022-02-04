using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.Models.DTOs
{
    public class CommentDTO
    {
        [Required(ErrorMessage = "Debe Agregar un Usuario")]
        public int IdUser { get; set; }
        [Required(ErrorMessage = "Debe Agregar una Noticia")]
        public int NewId { get; set; }
        [Required(ErrorMessage = "Body es requerido.")]
        [StringLength(maximumLength: 65535, ErrorMessage = "Body es demasiado largo.")]
        public string Body { get; set; }
    }
}