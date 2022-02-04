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
        Task<Result> Insert(CommentDTO commentDTO);
        void Update(Comment comment);
        void Delete(Comment comment);
    }
}