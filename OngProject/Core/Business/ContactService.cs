using Microsoft.Extensions.Configuration;
using OngProject.Core.Helper;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs;
using OngProject.Core.Models.Response;
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
        private readonly IConfiguration _config;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntityMapper _mapper;

        public ContactService(IConfiguration configuration, IUnitOfWork unitOfWork, IEntityMapper mapper)
        {
            _config = configuration;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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
                List<ContactDTO> dto = new();
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

        public async Task<Result> Insert(ContactDTO contactDto)
        {
            var contacts = _mapper.ContactDTOToContact(contactDto);

            // Validaciones
            if (string.IsNullOrEmpty(contacts.Email) && string.IsNullOrEmpty(contacts.Name))
                return Result.FailureResult("Debes proporcionar tu nombre y una dirección de correo electronico");
            
            if (string.IsNullOrEmpty(contacts.Email))
                return Result.FailureResult("Debes proporcionar un email");

            if (string.IsNullOrEmpty(contacts.Name))
                return Result.FailureResult("Debes proporcionar tu nombre");
            
            // Se Guarda el Contacto en la DB
            try
            {
                contacts.LastModified = DateTime.Now;
                await _unitOfWork.ContactRepository.Create(contacts);
                await _unitOfWork.SaveChangesAsync();

                //se envia mail de bienvenida
                var emailSender = new EmailSender(_config);
                var emailTitle = "Gracias por contactar con nosotros!";
                var emailBody = $"<h4>Hola {contacts.Name}</h4><p> Hemos recibido su mensaje, en breve nos pondremos en contacto con usted.</p>";
                var emailContact = string.Format("<a href='mailto:{0}'>{0}</a>", _config["MailParams:WelcomeMailContact"]);

                await emailSender.SendEmailWithTemplateAsync(contacts.Email, emailTitle, emailBody, emailContact);

                return Result<ContactDTO>.SuccessResult(_mapper.ContactToContactDTO(contacts));
            }
            catch(Exception ex)
            {
                return Result.FailureResult($"Error al guardar tu consult: {ex.Message}");
            }
        }

        public void Update(Contacts contacts)
        {
            throw new System.NotImplementedException();
        }
    }
}
