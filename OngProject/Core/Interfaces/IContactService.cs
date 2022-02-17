using OngProject.Core.Models.DTOs;
using OngProject.Core.Models.Response;
using OngProject.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface IContactService
    {
        public Task<ICollection<ContactDTO>> GetAll();
        public Contacts GetById();
        public Task<Result> Insert(ContactDTO contactDto);
        public void Update(Contacts contacts);
        public void Delete(Contacts contacts);
    }
}
