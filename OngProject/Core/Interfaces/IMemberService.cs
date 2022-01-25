using OngProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface IMemberService
    {
        public IEnumerable<Member> GetAll();
        public Member GetById();
        public void Insert(Member member);
        public void Update(Member member);
        public void Delete(Member member);
    }
}
