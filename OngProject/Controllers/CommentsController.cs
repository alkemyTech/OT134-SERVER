using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs;
using OngProject.Core.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    [Route("comments")]
    [ApiController]
    [Authorize]
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
        public async Task<Result> Post([FromBody] CommentDtoForRegister dto)
        {
            try
            {
                var claim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

                if (claim == null)
                    throw new Exception("Debe estar registrado para agregar un comentario");

                var response = await _commentsService.Insert(dto, Int32.Parse(claim.Value));
     
                return response;
            }
            catch (Exception ex)
            {
                return Result.FailureResult("Ocurrio un Problema : " + ex.ToString());
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CommentDtoForDisplay commentDto)
        {
            try
            {
                var claim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

                if (claim == null)
                    throw new Exception("Debe estar registrado para modificar un comentario");

                var result = await _commentsService.Update(id, Int32.Parse(claim.Value), commentDto);

                if(result.Success)
                    return Ok(result);

                return StatusCode(result.isError() ? 404 : 403, result);
            }
            catch(Exception ex)
            {
                return StatusCode(500, Result.ErrorResult(new List<string> { ex.Message }));
            }
        }

        [HttpDelete("{id}")]
        public async Task<Result> Delete(int id)
        {
            try
            {
                var claim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

                if (claim == null)
                    //throw new Exception("Debe estar registrado para borrar un comentario");
                    return Result.FailureResult("Debe estar registrado para borrar un comentario");

                var result = await _commentsService.Delete(id, Int32.Parse(claim.Value));
                return Result<Result>.SuccessResult(result);
            }
            catch (Exception ex)
            {

                return Result.FailureResult("Ocurrio un Problema : " + ex.ToString());
            }
        }

    }
}