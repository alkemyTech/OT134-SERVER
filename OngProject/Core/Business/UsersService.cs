using OngProject.Core.Interfaces;
using OngProject.Repositories.Interfaces;
using System;
using OngProject.Core.Models.DTOs;
using System.Threading.Tasks;
using OngProject.Core.Helper;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using OngProject.Core.Models.Response;

namespace OngProject.Core.Business
{
    public class UsersService : IUserService
    {
        private readonly IConfiguration _config;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntityMapper _mapper;
        private readonly IJwtHelper _jwtHelper;
        private readonly IImageService _imageService;
        public UsersService(IUnitOfWork unitOfWork,  IConfiguration configuration, IJwtHelper jwtHelper, IImageService imageService, IEntityMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _jwtHelper = jwtHelper;
            _config = configuration;
            _imageService = imageService;
        }

        public async Task<Result> GetAll()
        {
            try
            {
                var users = await _unitOfWork.UserRepository.FindAllAsync();

                var usersDTO = users
                        .Select(user => _mapper.UserToUserDto(user));

                return Result<IEnumerable<UserDTO>>.SuccessResult(usersDTO);
            }
            catch(Exception e)
            {
                return Result.ErrorResult(new List<string> { e.Message });
            }    
        }

        public async Task<Result> GetById(int id)
        {
            try
            {
                var user = await this._unitOfWork.UserRepository.GetByIdAsync(id);
                if(user != null)
                {
                    return Result<UserDetailDto>.SuccessResult(_mapper.UserToUserDetailDto(user));
                }

                return Result.FailureResult("Usuario inexistente.");
            }
            catch(Exception e)
            {
                return Result.ErrorResult(new List<string> { e.Message });
            }
        }

        public async Task<Result> Insert(UserRegisterDto dto)

        {
            var errorList = new List<string>();
            var user = _mapper.UserRegisterDtoToUser(dto);

            try
            {
                // verifico que no exista Email en sistema
                var existUser = await this._unitOfWork.UserRepository.FindByConditionAsync(x => x.Email == user.Email);
                if (existUser != null && existUser.Count > 0)
                {
                    return Result.FailureResult("Email ya existe en el sistema.");
                }

                user.Password = EncryptHelper.GetSHA256(user.Password);
                user.LastModified = DateTime.Today;
                user.SoftDelete = false;
                    
                user.Photo = await _imageService.UploadFile($"{Guid.NewGuid()}_{dto.Photo.FileName}", dto.Photo);                

                await this._unitOfWork.UserRepository.Create(user);
                await this._unitOfWork.SaveChangesAsync();                

                if (user.Rol == null)
                {
                    user.Rol =  await this._unitOfWork.RolRepository.GetByIdAsync(user.RolId);
                }                

                try
                {
                    //se envia mail de bienvenida
                    var emailSender = new EmailSender(_config);
                    var emailBody = $"<h4>Hola {user.FirstName} {user.LastName}</h4>{_config["MailParams:WelcomeMailBody"]}";
                    var emailContact = string.Format("<a href='mailto:{0}'>{0}</a>", _config["MailParams:WelcomeMailContact"]);

                    await emailSender.SendEmailWithTemplateAsync(user.Email, _config["MailParams:WelcomeMailTitle"], emailBody, emailContact);
                }
                catch(Exception e)
                {
                    errorList.Add($"No se envio email de bienvenida: { e.Message }");
                }

                var result = Result<string>.SuccessResult(_jwtHelper.GenerateJwtToken(user));
                result.ErrorList = errorList; // adjunto lista de posibles errores a la respuesta


                return Result<string>.SuccessResult(_jwtHelper.GenerateJwtToken(user));
            }
            catch(Exception e)
            {
                errorList.Add(e.Message);
                return Result.ErrorResult(errorList);
            }            
        }
 

        public async Task<Result> LoginAsync(UserLoginDTO userLoginDto)
        {
            try
            {
                var result = await this._unitOfWork.UserRepository.FindByConditionAsync(x => x.Email == userLoginDto.Email);

                if (result.Count > 0)
                {
                    var currentUser = result.FirstOrDefault();
                    if (currentUser != null)
                    {
                        var resultPassword = EncryptHelper.Verify(userLoginDto.Password, currentUser.Password);
                        if (resultPassword)
                        {
                            return Result<string>.SuccessResult(_jwtHelper.GenerateJwtToken(currentUser));
                        }                        
                    }                    
                }

                return Result.FailureResult("No se pudo iniciar sesion, usuario o contrasena invalidos");
            } 
            catch (Exception e)
            {
                return Result.ErrorResult(new List<string> { e.Message });
            }
        }


        public async Task<Result> Update(int id, UserUpdateDto userDto)
        {
            try
            { 
                var user = await _unitOfWork.UserRepository.GetByIdAsync(id);
                if (user is not null)
                {                
                    user.FirstName = userDto.FirstName;
                    user.LastName = userDto.LastName;
                    user.Email = userDto.Email;
                    user.LastModified = DateTime.Now;

                    if (userDto.Photo is not null)
                    {
                        await _imageService.AwsDeleteFile(user.Photo[(user.Photo.LastIndexOf("/") + 1)..]);
                        user.Photo = await _imageService.UploadFile($"{Guid.NewGuid()}_{userDto.Photo.FileName}", userDto.Photo);
                    }
                    await this._unitOfWork.SaveChangesAsync();
                    return Result<UserDtoForDisplay>.SuccessResult(_mapper.UserToUserDtoForDisplay(user), 200);
                }
                return Result.FailureResult("id de usuario inexistente.", 404);
            }  
            catch (Exception ex)
            {
                return Result.FailureResult("Error al actualizar el usuario: " + ex.Message, 500);
            }
        }

        public async Task<Result> Delete(int id)
        {
            try
            {
                var user = await this._unitOfWork.UserRepository.GetByIdAsync(id);
                if(user != null)
                {
                    if (user.SoftDelete) 
                    {                        
                        return Result.FailureResult($"id({user.Id}) ya eliminado del sistema.");
                    }
                    user.SoftDelete = true;
                    user.LastModified = DateTime.Today;
                    await this._unitOfWork.SaveChangesAsync();

                    return Result<string>.SuccessResult($"Usuario({user.Id}) eliminado exitosamente.");
                }

                return Result.FailureResult("id de usuario inexistente.");
            }
            catch(Exception e)
            {                
                return Result.ErrorResult(new List<string> { e.Message });
            }
        }
    }
}