using Microsoft.AspNetCore.Http;

namespace OngProject.Core.Models.DTOs
{
    public class UserUpdateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public IFormFile Photo { get; set; }
    }
}
