using OngProject.Core.Helper;
using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Business
{
    public class ContactService : IContactService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly EntityMapper _mapper;

        public ContactService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = new EntityMapper();
        }

        public void Delete(Contacts contacts)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ICollection<ContactDTO>> GetAll()
        {
            var response = await _unitOfWork.ContactRepository.FindAllAsync();
            if (response.Count == 0)
            {
                return null;
            }
            else
            {
                List<ContactDTO> dto = new List<ContactDTO>();
                foreach (var item in response)
                {
                    dto.Add(_mapper.ContactToContactDTO(item));
                }
                return dto;
            }
        }

        public Contacts GetById()
        {
            throw new System.NotImplementedException();
        }

        public async Task<ManagerResponse> Insert(ContactDTO contactDto)
        {
            var contacts = _mapper.ContactDTOToContact(contactDto);

            // Validaciones
            if (string.IsNullOrEmpty(contacts.Email) && string.IsNullOrEmpty(contacts.Name))
                return new ManagerResponse
                {
                    StatusCode = 400,
                    Message = "Debes proporcionar tu nombre y una dirección de correo electronico"
                };

            if (string.IsNullOrEmpty(contacts.Email))
                return new ManagerResponse
                {
                    StatusCode = 400,
                    Message = "Debes proporcionar un email"
                };

            if (string.IsNullOrEmpty(contacts.Name))
                return new ManagerResponse
                {
                    StatusCode = 400,
                    Message = "Debes proporcionar tu nombre"
                };
            
            // Se Guarda el Contacto en la DB
            try
            {
                contacts.LastModified = DateTime.Now;
                await _unitOfWork.ContactRepository.Create(contacts);
                await _unitOfWork.SaveChangesAsync();

                return new ManagerResponse
                {
                    StatusCode = 201,
                    Message = "El contacto se añadio correctamente"
                };
            }
            catch(Exception ex)
            {
                return new ManagerResponse
                {
                    StatusCode = 500,
                    Message = ex.Message 
                };
            }
        }

        public void Update(Contacts contacts)
        {
            throw new System.NotImplementedException();
        }
    }
}
