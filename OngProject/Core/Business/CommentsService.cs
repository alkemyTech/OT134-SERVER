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
                    var ListComments = response.Where(x => x.NewId == IdNew).OrderBy(x => x.LastModified)
                                               .Select(x => _mapper.CommentToCommentDTO(x));
                    List<CommentDTO> dto = new List<CommentDTO>();
                    foreach (var item in ListComments)
                    {
                        dto.Add(item);
                    }
                    return Result<ICollection<CommentDTO>>.SuccessResult(dto);
                }
            }
            catch (Exception ex)
            {

                return Result.FailureResult("Ocurrio un Probelma al listar los resultados : " + ex.ToString());
            }

        }

        public async Task<Result> Insert(CommentDTO commentDTO)
        {
            try
            {
                if (commentDTO.UserId == 0)
                {
                    return Result.FailureResult("Se debe agregar un usuario");
                }
                else if (commentDTO.NewId == 0)
                {
                    return Result.FailureResult("Se debe agregar una noticia");
                }
                else if (commentDTO.Body.Equals(""))
                {
                    return Result.FailureResult("Se debe agregar un Comentario");
                }
                else
                {
                    var result = _mapper.CommentDTOToComment(commentDTO);
                    result.LastModified = DateTime.Today;
                    await _unitOfWork.CommentsRepository.Create(result);
                    await _unitOfWork.SaveChangesAsync();

                    return Result<CommentDTO>.SuccessResult(commentDTO);
                }

            }
            catch (Exception ex)
            {

                return Result.FailureResult("Ocurrio un problema al momento de agregar un Comentario : " + ex.ToString());
            }
        }

        public void Update(Comment comment)
        {
            throw new NotImplementedException();
        }

        public void Delete(Comment comment)
        {
            throw new NotImplementedException();
        }

    }
}