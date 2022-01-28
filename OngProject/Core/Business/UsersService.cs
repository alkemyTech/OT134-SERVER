using OngProject.Core.Interfaces;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using OngProject.Core.Mapper;
using OngProject.Core.Models.DTOs;
using System.Threading.Tasks;
using OngProject.Core.Helper;

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
                    return null;
                }

                user.Password = EncryptHelper.GetSHA256(user.Password);

                await this._unitOfWork.UserRepository.Create(user);
                await this._unitOfWork.SaveChangesAsync();

                return user;
            }
            catch { }

            return null;
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