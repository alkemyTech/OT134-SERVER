﻿using OngProject.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OngProject.Core.Models.DTOs
{
    public class SlideDtoById
    {
        [Display(Name = "UrlDeImagen")]
        public string ImageUrl { get; set; }

        [Display(Name = "Texto")]
        [StringLength(2000)]
        public string Text { get; set; }

        [Display(Name = "Orden")]
        [StringLength(255)]
        public int Order { get; set; }

        [Display(Name = "IdDeOrganizacion")]
        public int OrganizationId { get; set; }
    }
}
