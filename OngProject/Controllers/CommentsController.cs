using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs;
using OngProject.Core.Models.Response;
using System;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    [Route("comments")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentsService _commentsService;

        public CommentsController(ICommentsService commentsService)
        {
            _commentsService = commentsService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var commentDTO = await _commentsService.GetAll();
                if (commentDTO != null)
                {
                    return Ok(commentDTO);
                }
                else return NotFound("No se encontró información sobre los comentarios");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [Route("/news/:id/comments")]
        [HttpGet]
        public async Task<Result> Get(int id)
        {
            try
            {
                var response = await _commentsService.GetById(id);
                return (Result)response;
            }
            catch (Exception ex)
            {

                return Result.FailureResult("Ocurrio un Problema" + ex.ToString());
            }
        }

        [HttpPost]
        public async Task<Result> Post([FromBody] CommentDTO dto)
        {
            try
            {
                var response = await _commentsService.Insert(dto);
                return response;
            }
            catch (Exception ex)
            {

                return Result.FailureResult("Ocurrio un Problema : " + ex.ToString());
            }
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id},{idUser}")]
        public async Task<Result> Delete(int id,int idUser)
        {
            try
            {
                var result = await _commentsService.Delete(id,idUser);
                return Result<Result>.SuccessResult(result);
            }
            catch (Exception ex)
            {

                return Result.FailureResult("Ocurrio un Problema : " + ex.ToString());
            }
        }
    }
}