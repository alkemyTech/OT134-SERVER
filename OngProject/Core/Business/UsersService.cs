using OngProject.Core.Interfaces;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using OngProject.Core.Mapper;
using OngProject.Core.Models.DTOs;
using System.Threading.Tasks;
using OngProject.Core.Helper;
using Microsoft.AspNetCore.Mvc;

namespace OngProject.Core.Business
{
    public class UsersService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UsersService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public User GetById()
        {
            throw new NotImplementedException();
        }

        public async Task<User> Insert(UserRegisterDto dto)
        {
            var user = UserMapper.mapUserRegisterDtoToUser(dto);

            try
            {
                // verifico que no exista Email en sistema
                var result = await this._unitOfWork.UserRepository.FindByConditionAsync(x => x.Email == user.Email);
                if (result != null && result.Count > 0)
                {
                    throw new Exception("Email ya existe en el sistema.");
                }

                user.Password = EncryptHelper.GetSHA256(user.Password);

                await this._unitOfWork.UserRepository.Create(user);
                await this._unitOfWork.SaveChangesAsync();

                return user;
            }
            catch(Exception e)
            {
                throw new Exception("Usuario no registrado: " + e.ToString());
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
    public class UsersService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly EntityMapper _mapper;
        public UsersService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = new EntityMapper();
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

        public void Insert(User user)
        {
            throw new NotImplementedException();
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