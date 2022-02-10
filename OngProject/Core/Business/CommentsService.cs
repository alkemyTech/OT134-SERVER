using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs;
using OngProject.Core.Models.Response;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Business
{
    public class CommentsService : ICommentsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntityMapper _mapper;

        public CommentsService(IUnitOfWork unitOfWork, IEntityMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CommentDtoForDisplay>> GetAll()
        {
            var comments = await _unitOfWork.CommentsRepository.FindAllAsync();

            var commentsDTO = comments
                .OrderBy(comment => comment.LastModified)
                .Select(comment => _mapper.CommentToCommentDtoForDisplay(comment));

            return commentsDTO;
        }

        public async Task<Result> GetById(int IdNew)
        {

            try
            {
                if (IdNew == 0)
                {
                    return Result.FailureResult("Debe seleccionar una novedad");
                }
                else
                {
                    var response = await _unitOfWork.CommentsRepository.FindAllAsync();
                    var ListComments = response.Where(x => x.NewId == IdNew && x.SoftDelete == false).OrderBy(x => x.LastModified)
                                               .Select(x => _mapper.CommentToCommentDtoForDisplay(x));

                    List<CommentDtoForDisplay> dto = new();
                    foreach (var item in ListComments)
                    {
                        dto.Add(item);
                    }
                    return Result<ICollection<CommentDtoForDisplay>>.SuccessResult(dto);
                }
            }
            catch (Exception ex)
            {

                return Result.FailureResult("Ocurrio un Probelma al listar los resultados : " + ex.ToString());
            }

        }

        public async Task<Result> Insert(CommentDtoForRegister commentDTO, int UserId)
        {
            try
            {
                if (commentDTO.NewId == 0)
                {
                    return Result.FailureResult("Se debe agregar una noticia");
                }
                else if (commentDTO.Body.Equals(""))
                {
                    return Result.FailureResult("Se debe agregar un Comentario");
                }
                else
                {
                    var result = _mapper.CommentForRegisterToComment(commentDTO);
                    result.UserId = UserId;
                    result.LastModified = DateTime.Today;
                    await _unitOfWork.CommentsRepository.Create(result);
                    await _unitOfWork.SaveChangesAsync();

                    var commentDisplay = _mapper.CommentToCommentDtoForDisplay(result); 

                    return Result<CommentDtoForDisplay>.SuccessResult(commentDisplay);
                }

            }
            catch (Exception ex)
            {

                return Result.FailureResult("Ocurrio un problema al momento de agregar un Comentario : " + ex.ToString());
            }
        }

        public async Task<Result> Update(int idComment, int idUser, CommentDtoForDisplay commentDto)
        {
            try
            {
                var response = await _unitOfWork.CommentsRepository.GetByIdAsync(idComment);

                if (response == null || response.SoftDelete)
                    return Result.FailureResult("No se encontro comentario");
                else
                {
                    var user = await _unitOfWork.CommentsRepository.FindByConditionAsync(x => x.UserId == idUser && x.Id == idComment);
                    
                    var rolUser = await _unitOfWork.UserRepository.GetByIdAsync(idUser);

                    if (user.Count == 0 && rolUser.Rol.Name == "User")
                        return Result.FailureResult("Usted no tiene permiso para modificar este comentario");

                    response.Body = commentDto.Body;
                    response.LastModified = DateTime.Today;

                    await _unitOfWork.SaveChangesAsync();

                    var commentDisplay = _mapper.CommentToCommentDtoForDisplay(response);

                    return Result<CommentDtoForDisplay>.SuccessResult(commentDisplay);
                }
            }
            catch(Exception ex)
            {
                return Result.FailureResult(ex.Message);
            }
        }

        public async Task<Result> Delete(int IdComment, int idUser)
        {
            try
            {
                var result = await _unitOfWork.CommentsRepository.GetByIdAsync(IdComment);
                var VerifyAdminUser = await _unitOfWork.UserRepository.GetByIdAsync(idUser);
                if (result == null)
                {
                    return Result.FailureResult("Error 404 - Comentario no encontrado");
                }
                if (result.SoftDelete)
                {
                    return Result.FailureResult("Error 404 - Comentario no encontrado");
                }
                else if (VerifyAdminUser.RolId == 2)
                {
                    result.SoftDelete = true;
                    result.LastModified = DateTime.Now;

                    await _unitOfWork.SaveChangesAsync();

                    return Result<Comment>.SuccessResult(result);
                }
                else if (result.UserId == idUser)
                {
                    result.SoftDelete = true;
                    result.LastModified = DateTime.Now;

                    await this._unitOfWork.SaveChangesAsync();

                    return Result<Comment>.SuccessResult(result);
                }
                else
                {
                    return Result.FailureResult("Error 403 - Usted no tiene permiso para borrar este comentario");
                }
            }
            catch (Exception ex)
            {
                return Result.FailureResult("Ocurrio un Problema al eliminar el comentario : " + ex.ToString());
            }
        }

    }
}