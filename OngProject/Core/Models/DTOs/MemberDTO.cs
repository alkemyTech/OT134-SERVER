﻿using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.Models.DTOs
{
    public class MemberDTO
    {
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El nombre es requerido")]
        [StringLength(maximumLength: 255, ErrorMessage = "El nombre es demasiado largo")]
        public string Name { get; set; }

        [Display(Name = "Descripción")]
        [StringLength(maximumLength: 255, ErrorMessage = "La descripción es demasiado larga")]
        public string Description { get; set; }

        [Required]
        public IFormFile File { get; set; }
    }
}