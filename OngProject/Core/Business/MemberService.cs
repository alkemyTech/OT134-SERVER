using OngProject.Core.Interfaces;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OngProject.Core.Models.DTOs;
using OngProject.Core.Models.Response;
using OngProject.Core.Helper;

namespace OngProject.Core.Business
{
    public class MemberService : IMemberService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntityMapper _mapper;
        private readonly ImageService _imageService;

        public MemberService(IUnitOfWork unitOfWork, IEntityMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _imageService = new ImageService(_unitOfWork);
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

        public async Task<Result> Insert(MemberDTO memberDTO)
        {
            try
            {
                var member = _mapper.MemberDTOToMember(memberDTO);

                var resultName = await _unitOfWork.MembersRepository.FindByConditionAsync(x => x.Name == memberDTO.Name);
                
                if (resultName.Count == 0)
                {
                    var aws = new S3AwsHelper();
                    var result = await _imageService.UploadFile($"{Guid.NewGuid()}_{memberDTO.File.FileName}", memberDTO.File);

                    member.SoftDelete = false;
                    member.LastModified = DateTime.Now;

                    await _unitOfWork.MembersRepository.Create(member);
                    await _unitOfWork.SaveChangesAsync();

                    var memberCalss = new Member
                    {
                        Name = result,
                        Description = memberDTO.Description,
                    };
                    return Result<Member>.SuccessResult(memberCalss);  
                }               
                else
                {
                    throw new Exception("El nombre ya existe en el sistema, intente uno diferente al ingresado.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Miembro no registrado: " + ex.Message);
            }
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