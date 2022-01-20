using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OngProject.Entities
{
    public class EntityBase
    {
        [Key]
        public int Id { get; set; }
        public DateTime LastModified { get; set; }
        public bool SoftDelete { get; set; }
    }
}
