﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.Models.DTOs
{
    public class OrganizationDTO
    {
        [Required(ErrorMessage = "The Name Is Required")]
        [StringLength(maximumLength: 255, ErrorMessage = "The Name Is Too Long")]
        public string Name { get; set; }
        [Required(ErrorMessage = "The Image Is Required")]
        [StringLength(maximumLength: 255, ErrorMessage = "The Image Is Too Long")]
        public string Image { get; set; }
        [StringLength(maximumLength: 255, ErrorMessage = "The Address Is Too Long")]
        public string Address { get; set; }
        [StringLength(maximumLength: 20, ErrorMessage = "The Phone Number Is Too Long")]
        public int? Phone { get; set; }
    }
}