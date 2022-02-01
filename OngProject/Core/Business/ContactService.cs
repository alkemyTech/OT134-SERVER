﻿using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
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
