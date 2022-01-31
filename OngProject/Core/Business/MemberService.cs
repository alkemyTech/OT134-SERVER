using OngProject.Core.Interfaces;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OngProject.Core.Models.DTOs;
using OngProject.Core.Mapper;

namespace OngProject.Core.Business
{
    public class MemberService : IMemberService
    {
        private readonly IUnitOfWork _unitOfWork; 
        private readonly EntityMapper _mapper;

        public MemberService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = new EntityMapper();
        }

        public async Task<IEnumerable<MemberDTO>> GetAll()
        {
            var response = await _unitOfWork.MembersRepository.FindAllAsync();
            return _mapper.MemberToMemberDTO(response.FirstOrDefault());
        }

        public Member GetById()
        {
            throw new NotImplementedException();
        }

        public void Insert(Member member)
        {
            throw new NotImplementedException();
        }

        public void Update(Member member)
        {
            throw new NotImplementedException();
        }

        public void Delete(Member member)
        {
            throw new NotImplementedException();
        }       
    }
}