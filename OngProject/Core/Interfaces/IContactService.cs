using OngProject.Core.Models.DTOs;
using OngProject.Core.Models.Response;
using OngProject.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface IContactService
    {
        Task<ICollection<ContactDTO>> GetAll();
        Contacts GetById();
        Task<Result> Insert(ContactDTO contactDto);
        void Update(Contacts contacts);
        void Delete(Contacts contacts);
    }
}
