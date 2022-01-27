using OngProject.Core.Interfaces;
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

        public CommentsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<Comment> GetAll()
        {
            return await _unitOfWork.CommentsRepository.FindAllAsync();
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