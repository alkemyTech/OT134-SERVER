using OngProject.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface ICommentsService
    {
        Task<Comment> GetAll();
        Comment GetById();
        void Insert(Comment comment);
        void Update(Comment comment);
        void Delete(Comment comment);
    }
}