using System.Collections.Generic;

namespace OngProject.Core.Models.DTOs
{
    public class OrganizationDTOForDisplay
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public string Address { get; set; }
        public int? Phone { get; set; }
        public ICollection<SlideDtoForDisplay> Slides { get; set; }
    }
}
