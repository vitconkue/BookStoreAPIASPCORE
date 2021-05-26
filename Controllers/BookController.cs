using Microsoft.AspNetCore.Mvc;
using BookStore.Models;
using BookStore.Repository;
using Microsoft.Extensions.Logging;
using BookStore.Errors;
using BookStore.ActionModels; 

namespace BookStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController: ControllerBase
    {
        private readonly ILogger<BookController> _logger;
        private readonly IBookRepository _repository;

        public BookController(IBookRepository repository, ILogger<BookController> logger)
        {
            _logger = logger; 

            _repository  = repository;
        }
        [HttpGet]
        public IActionResult GetAllBook()
        {
            _logger.LogInformation("A client is getting all books"); 
            var result = _repository.GetAllBooks(); 
            _logger.LogCritical($"Found {result.Count} book(s)"); 

            if(result.Count == 0)
            {
                return NotFound(new NoBookFoundError()); 
            }
            else return Ok(result); 
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetSingleBook(int id)
        {
            var result = _repository.GetById(id);
            if(result == null)
            {
                return NotFound(new NoBookFoundError());
            }
            return Ok(result); 
        }

        [HttpPost] 
        public IActionResult AddBook([FromBody] AddBookModel model)
        {
            


            return Ok();
        }
    }
}