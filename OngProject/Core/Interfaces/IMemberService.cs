﻿using OngProject.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using OngProject.Core.Models.DTOs;
using OngProject.Core.Models.Response;

namespace OngProject.Core.Interfaces
{
    public interface IMemberService
    {
        Task<IEnumerable<MemberDTO>> GetAll();
        Member GetById();
        Task<Result> Insert(MemberDTO memberDTO);
        void Update(Member member);
        Task<Result> Delete(int id);
    }
}