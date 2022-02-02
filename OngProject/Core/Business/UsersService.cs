using OngProject.Core.Interfaces;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System;
using OngProject.Core.Mapper;
using OngProject.Core.Models.DTOs;
using System.Threading.Tasks;
using OngProject.Core.Helper;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

using OngProject.Core.Models;
using System.IdentityModel.Tokens.Jwt;

using System.Collections.Generic;


namespace OngProject.Core.Business
{
    public class UsersService : IUserService
    {
        private readonly IConfiguration _config;
        private readonly IUnitOfWork _unitOfWork;
        private readonly EntityMapper _mapper;
        private readonly JwtHelper _jwtHelper;
        public UsersService(IUnitOfWork unitOfWork,  IConfiguration configuration, JwtHelper jwtHelper)
        {
            _unitOfWork = unitOfWork;
            _mapper = new EntityMapper();
            _jwtHelper = jwtHelper;
            _config = configuration;
        }

        public async Task<IEnumerable<UserDTO>> GetAll()
        {
            var users = await _unitOfWork.UserRepository.FindAllAsync();

            var usersDTO = users
                .Select(user => _mapper.UserToUserDto(user));

            return usersDTO;
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
 

        public async Task<UserDetailDto> LoginAsync(UserLoginDTO userLoginDto)
        {
            try
            {
                var result = await this._unitOfWork.UserRepository.FindByConditionAsync(x => x.Email == userLoginDto.Email);

                if (result.Count > 0)
                {
                    var currentUser = result.FirstOrDefault();
                    if (currentUser == null)
                    {
                        throw new Exception("No se pudo iniciar sesion, usuario o contrasena invalidos");
                    }
                    var resultPassword = EncryptHelper.Verify(userLoginDto.Password, currentUser.Password);

                    if (!resultPassword)
                    {
                        throw new Exception("No se pudo iniciar sesion, usuario o contrasena invalidos");
                    }

                    //No esta devolviendo el token
                    //var jwtSecurityToken = _jwtHelper.GenerateJwtToken(currentUser);

                    return _mapper.UseToUserDetailDto(currentUser);
                }
                else
                {
                    throw new Exception("Error al iniciar sesion");
                }
            } 
            catch (Exception e)
            {

                throw new Exception(e.Message);
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