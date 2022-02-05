using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
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
        private readonly EntityMapper _mapper;

        public CommentsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = new EntityMapper();
        }

        public async Task<IEnumerable<CommentDTO>> GetAll()
        {
            var comments = await _unitOfWork.CommentsRepository.FindAllAsync();

            var commentsDTO = comments
                .OrderBy(comment => comment.LastModified)
                .Select(comment => _mapper.CommentToCommentDTO(comment));

            return commentsDTO;
        }

        public Comment GetById()
        {
            throw new NotImplementedException();
        }

        public void Insert(Comment comment)
        {
            throw new NotImplementedException();
        }

        public void Update(Comment comment)
        {
            throw new NotImplementedException();
        }

        public async Task<Result> Delete(int IdComment,int idUser)
        {
            try
            {
                var result = await _unitOfWork.CommentsRepository.GetByIdAsync(IdComment);
                var VerifyAdminUser = await _unitOfWork.UserRepository.GetByIdAsync(idUser);
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