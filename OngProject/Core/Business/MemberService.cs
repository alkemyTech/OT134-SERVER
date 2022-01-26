using OngProject.Core.Interfaces;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Business
{
    public class MemberService : IMemberService
    {
        private readonly IUnitOfWork _unitOfWork;
        public MemberService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Member> GetAll()
        {
            throw new NotImplementedException();
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
