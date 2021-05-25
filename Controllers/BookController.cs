using Microsoft.AspNetCore.Mvc;
using BookStore.Models;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController: ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllBook()
        {
            return Redirect("/"); 
        }
    }
}