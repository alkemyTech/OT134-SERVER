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

        public void Insert(Comment comment)
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
    }
}