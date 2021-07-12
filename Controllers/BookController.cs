using Microsoft.AspNetCore.Mvc;
using BookStore.Models;
using BookStore.Repository;
using Microsoft.Extensions.Logging;
using BookStore.Errors;
using BookStore.ActionModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using BookStore.DTO;
using System.Linq;

namespace BookStore.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;
        private readonly IBookRepository _repository;

        public BookController(IBookRepository repository, ILogger<BookController> logger)
        {
            _logger = logger;

            _repository = repository;
        }
        [HttpGet]
        public IActionResult GetAllBook([FromQuery] string typeId, [FromQuery] string searchString)
        {
            _logger.LogInformation("A client is getting books");
            var result = _repository.GetAllBooks( typeId,searchString);
            _logger.LogCritical($"Found {result.Count} book(s)");

            if (result.Count == 0)
            {
                return NotFound(new NoBookFoundError());
            }
            else return Ok(result.Select(book => new BookDTO(book)));
        }
        [HttpGet]
        [Route("types")]
        public IActionResult GetAllTypes()
        {
            return Ok(_repository.GetAllType());
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetSingleBook(int id)
        {
            var result = _repository.GetById(id);
            if (result == null)
            {
                return NotFound(new NoBookFoundError());
            }
            return Ok(new BookDTO(result));
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] AddBookModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Book result = _repository.AddBook(model);

            return Ok(new BookDTO(result));
        }

        [HttpPost]
        [Route("delete/{id}")]
        public IActionResult DeleteBook(int id)
        {
            var result = _repository.DeleteById(id);

            if (result == null)
            {
                return NotFound(new NoBookFoundError());
            }
            return Ok();
        }
        [HttpPost]
        [Route("update")]
       
        public IActionResult UpdateBook([FromBody] UpdateBookActionModel model)
        {

            Book result = _repository.UpdateBook(model);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(new BookDTO(result));
        }
    }
}