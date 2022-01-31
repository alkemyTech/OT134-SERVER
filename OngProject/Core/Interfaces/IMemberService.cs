using OngProject.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using OngProject.Core.Models.DTOs;

namespace OngProject.Core.Interfaces
{
    public interface IMemberService
    {
        Task<IEnumerable<MemberDTO>> GetAll();
        public Member GetById();
        public void Insert(Member member);
        public void Update(Member member);
        public void Delete(Member member);
    }
}