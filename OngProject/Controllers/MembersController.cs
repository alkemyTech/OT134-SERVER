using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OngProject.Repositories.Interfaces;

namespace OngProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public MembersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
        
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok();
        }
        
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }
        
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }
        
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
