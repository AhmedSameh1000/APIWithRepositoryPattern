using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositorypatern.UnitOfWork.Core.Interfaces;
using Repositorypatern.UnitOfWork.Core.Models;
using Repositorypatern.UnitOfWork.Core.UnitOfWork;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public BooksController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetById()
        {
            return Ok(_unitOfWork.Books.GetById(2));
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            return Ok(_unitOfWork.Books.GetAll());
        }

        [HttpGet("GetByName")]
        public IActionResult GetByName()
        {
            return Ok(_unitOfWork.Books.Find(c => c.Title == "Good", new[] { "Author" }));
        }

        [HttpGet("GetAllWithAuthores")]
        public IActionResult GetAllWithAuthores()
        {
            return Ok(_unitOfWork.Books.FindAll(c => c.Id > 2, new[] { "Author" }));
        }

        [HttpGet("GetOrder")]
        public IActionResult GetOrder()
        {
            return Ok(_unitOfWork.Books.FindAll(c => c.Id > 2, take: null, skip: null, orderBy: b => b.Id));
        }

        [HttpPost("AddOne")]
        public IActionResult AddOne()
        {
            return Ok(_unitOfWork.Books.Add(new Book
            {
                Title = "test 3",
                AuthorId = 1
            }));
        }
    }
}