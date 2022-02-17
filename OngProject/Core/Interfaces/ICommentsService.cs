using OngProject.Core.Models.DTOs;
using OngProject.Core.Models.Response;
using OngProject.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface ICommentsService
    {
        Task<IEnumerable<CommentDtoForDisplay>> GetAll();
        Task<Result> GetById(int id);
        Task<Result> Insert(CommentDtoForRegister commentDTO, int idUser);
        Task<Result> Update(int idComment, int idUser, CommentDtoForDisplay commentDto);
        Task<Result> Delete(int IdComment,int idUser);
    }
}