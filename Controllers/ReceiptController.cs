

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookStore.Repository;

namespace BookStore.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ReceiptController: ControllerBase
    {
        private readonly IReceiptRepository _repo;

        public ReceiptController(IReceiptRepository repo)
        {
            _repo = repo; 
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReceipt()
        {
            var result = await _repo.GetAll(); 
            return Ok(); 
        }
    }

}