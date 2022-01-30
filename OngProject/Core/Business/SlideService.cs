﻿using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Business
{
    public class SlideService : IContactService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly EntityMapper _mapper;

        public SlideService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = new EntityMapper();
        }

        public void Delete(Contacts contacts)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ContactDTO> GetAll()
        {
            var response = await _unitOfWork.ContactRepository.FindAllAsync();
            return _mapper.ContactToContactDTO(response.FirstOrDefault());
        }

        public Contacts GetById()
        {
            throw new System.NotImplementedException();
        }

        public void Insert(Contacts contacts)
        {
            throw new System.NotImplementedException();
        }

        public void Update(Contacts contacts)
        {
            throw new System.NotImplementedException();
        }
    }
}
