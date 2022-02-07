using OngProject.Core.Interfaces;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OngProject.Core.Models.DTOs;
using OngProject.Core.Models.Response;

namespace OngProject.Core.Business
{
    public class MemberService : IMemberService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntityMapper _mapper;

        public MemberService(IUnitOfWork unitOfWork, IEntityMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MemberDTO>> GetAll()
        {
            var members = await _unitOfWork.MembersRepository.FindAllAsync();

            var membersDTO = members
                .Select(member => _mapper.MemberToMemberDTO(member))
                .ToList();

            return membersDTO;
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

        public async Task<Result> Delete(int id)
        {
            try
            {
                var member = await _unitOfWork.MembersRepository.GetByIdAsync(id);

                if (member != null)
                {
                    if (member.SoftDelete)
                        return Result.FailureResult("El miembro ya se encuentra eliminado del sistema");

                    member.SoftDelete = true;
                    member.LastModified = DateTime.Now;
                    await _unitOfWork.SaveChangesAsync();

                    return Result<Member>.SuccessResult(member);
                }
                return Result.FailureResult("No existe un miembro con ese Id");
            }
            catch (Exception ex)
            {
                return Result.FailureResult($"Error al eliminar al miembro: {ex.Message}");
            }
        }
    }
}