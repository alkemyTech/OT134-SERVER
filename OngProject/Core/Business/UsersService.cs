using OngProject.Core.Interfaces;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System;
using OngProject.Core.Mapper;
using OngProject.Core.Models.DTOs;
using System.Threading.Tasks;
using OngProject.Core.Helper;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace OngProject.Core.Business
{
    public class UsersService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly EntityMapper _mapper;
        private readonly IConfiguration _config;
        public UsersService(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = new EntityMapper();
            _config = configuration;
        }

        public async Task<UserDTO> GetAll()
        {
            var response = await _unitOfWork.UserRepository.FindAllAsync();
            return _mapper.UserToUserDto(response.FirstOrDefault());
        }

        public User GetById()
        {
            throw new NotImplementedException();
        }

        public async Task<UserDetailDto> Insert(UserRegisterDto dto)
        {
            var user = _mapper.UserRegisterDtoToUser(dto);

            try
            {
                // verifico que no exista Email en sistema
                var result = await this._unitOfWork.UserRepository.FindByConditionAsync(x => x.Email == user.Email);
                if (result != null && result.Count > 0)
                {
                    throw new Exception("Email ya existe en el sistema.");
                }

                user.Password = EncryptHelper.GetSHA256(user.Password);
                user.LastModified = DateTime.Today;
                user.SoftDelete = false;

                await this._unitOfWork.UserRepository.Create(user);
                await this._unitOfWork.SaveChangesAsync();

                //se envia mail de bienvenida
                var emailSender = new EmailSender(_config);
                var emailBody = $"<h4>Hola {user.FirstName} {user.LastName}</h4>{_config["MailParams:WelcomeMailBody"]}";
                var emailContact = string.Format("<a href='mailto:{0}'>{0}</a>", _config["MailParams:WelcomeMailContact"]);
                
                await emailSender.SendEmailWithTemplateAsync(user.Email, _config["MailParams:WelcomeMailTitle"], emailBody, emailContact);

                return _mapper.UseToUserDetailDto(user);
            }
            catch(Exception e)
            {
                throw new Exception("Usuario no registrado: " + e.Message);
            }            
        }

        public void Update(User user)
        {
            throw new NotImplementedException();
        }

        public void Delete(User user)
        {
            throw new NotImplementedException();
        }
    }
}