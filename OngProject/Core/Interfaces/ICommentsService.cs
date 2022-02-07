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
        Task<Result> GetById(int id);
        Task<Result> Insert(CommentDTO commentDTO);
        void Update(Comment comment);
        void Delete(Comment comment);
    }
}