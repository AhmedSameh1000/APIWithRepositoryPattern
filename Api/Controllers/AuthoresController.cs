using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositorypatern.UnitOfWork.Core.Interfaces;
using Repositorypatern.UnitOfWork.Core.Models;
using Repositorypatern.UnitOfWork.Core.UnitOfWork;
using Repositorypatern.UnitOfWork.EF.Repositories;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthoresController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthoresController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetById()
        {
            return Ok(_unitOfWork.Authers.GetById(1));
        }

        [HttpGet("GetByIdAsync")]
        public IActionResult GetByIdAsync()
        {
            return Ok(_unitOfWork.Authers.GetById(1));
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            return Ok(_unitOfWork.Authers.GetAll());
        }
    }
}