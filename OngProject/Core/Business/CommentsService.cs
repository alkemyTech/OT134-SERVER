using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
using OngProject.Core.Models.DTOs;
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


        public void Update(Comment comment)
        {
            throw new NotImplementedException();
        }

        public void Delete(Comment comment)
        {
            throw new NotImplementedException();
        }

        public async Task<CommentDTO> Insert(CommentDTO commentDTO)
        {

            try
            {
                if (commentDTO.IdUser == 0)
                {
                    throw new Exception("Se debe Agregar un usuario para agregar un comentario");
                }
                else if (commentDTO.NewId == 0)
                {
                    throw new Exception("Se debe seleccionar una noticia para agregar un comentario");
                }
                else 
                {
                    var comment = _mapper.CommentDTOToComment(commentDTO);

                    await _unitOfWork.CommentsRepository.Create(comment);
                    await _unitOfWork.SaveChangesAsync();

                    return commentDTO;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Comentario no registrado: " + ex.Message);
            }
        }
    }
}