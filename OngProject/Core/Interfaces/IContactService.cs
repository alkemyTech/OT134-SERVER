using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface IContactService
    {
        public Task<ContactDTO> GetAll();
        public Contacts GetById();
        public void Insert(Contacts contacts);
        public void Update(Contacts contacts);
        public void Delete(Contacts contacts);
    }
}
