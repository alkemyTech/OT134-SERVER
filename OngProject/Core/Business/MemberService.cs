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
using OngProject.Core.Models.PagedResourceParameters;
using OngProject.Core.Models.Paged;
using Microsoft.AspNetCore.Http;

namespace OngProject.Core.Business
{
    public class MemberService : IMemberService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntityMapper _entityMapper;
        private readonly ImageService _imageService;
        private readonly IHttpContextAccessor _httpContext;

        public MemberService(IUnitOfWork unitOfWork, 
                             IEntityMapper mapper, 
                             IHttpContextAccessor httpContext)
        {
            _unitOfWork = unitOfWork;
            _entityMapper = mapper;
            _imageService = new ImageService(_unitOfWork);
            _httpContext = httpContext;
        }

        public async Task<Result> GetAll(PaginationParams paginationParams)
        {
            try
            {
                var members = await _unitOfWork.MembersRepository.FindAllAsync(null, null, null, paginationParams.PageNumber, paginationParams.PageSize);
                var totalMembers = await _unitOfWork.MembersRepository.Count();

                if (totalMembers == 0)
                {
                    return Result.FailureResult("No existen Miembros que mostrar");
                }

                if (members.Count == 0)
                {
                    return Result.FailureResult("paginacion invalida, no hay resultados disponibles");
                }

                var Dto = members
                    .Select(member => _entityMapper.MemberToMemberDTODisplay(member));

                var paged = PagedList<MemberDTODisplay>.Create(Dto.ToList(), totalMembers,
                                                                paginationParams.PageNumber,
                                                                paginationParams.PageSize);

                var url = $"{this._httpContext.HttpContext.Request.Scheme}://{this._httpContext.HttpContext.Request.Host}{this._httpContext.HttpContext.Request.Path}";
                var pagedResponse = new PagedResponse<MemberDTODisplay>(paged, url);

                return Result<PagedResponse<MemberDTODisplay>>.SuccessResult(pagedResponse);
            }
            catch (Exception e)
            {
                return Result.ErrorResult(new List<string> { e.Message });
            }
        }

        public Member GetById()
        {
            throw new NotImplementedException();
        }

        public async Task<Result> Insert(MemberDTORegister memberDTO)
        {
            try
            {
                var member = _entityMapper.MemberDTORegisterToMember(memberDTO);

                var resultName = await _unitOfWork.MembersRepository.FindByConditionAsync(x => x.Name == memberDTO.Name);

                if (resultName.Count == 0)
                {
                    var aws = new S3AwsHelper();
                    var result = await _imageService.UploadFile($"{Guid.NewGuid()}_{memberDTO.File.FileName}", memberDTO.File);

                    member.SoftDelete = false;
                    member.LastModified = DateTime.Now;
                    member.Image = result;

                    await _unitOfWork.MembersRepository.Create(member);
                    await _unitOfWork.SaveChangesAsync();

                    var memberDisplay = new MemberDTODisplay
                    {
                        Image = result,
                        Name = memberDTO.Name,
                        Description = memberDTO.Description,
                    };
                    return Result<MemberDTODisplay>.SuccessResult(memberDisplay);
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

                    return Result<string>.SuccessResult($"Miembro:({member.Id}) ha sido eliminado exitosamente.");
                   
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