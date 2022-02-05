using OngProject.Core.Models.DTOs;
using OngProject.Core.Models.Response;
using OngProject.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface ICommentsService
    {
        Task<IEnumerable<CommentDTO>> GetAll();
        Comment GetById();
        void Insert(Comment comment);
        void Update(Comment comment);
        Task<Result> Delete(int IdComment,int idUser);
    }
}