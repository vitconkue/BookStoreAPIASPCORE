

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookStore.Repository;
using System.Linq;
using BookStore.DTO;
using BookStore.ActionModels;

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
            return Ok(result.Select(receipt => new ReceiptDTO(receipt))); 
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetSingleReceipt(int id)
        {
            var result = await _repo.GetSingleReceipt(id); 
            if(result == null)
                return NotFound();
            return Ok(new ReceiptDTO(result));
        }

        [HttpPost]
        public async Task<IActionResult> AddReceipt([FromBody] AddReceiptActionModel model)
        {
            var result = await _repo.AddReceipt(model); 

            if(result == null)
                return BadRequest(); 
            return Ok(new ReceiptDTO(result)); 

        }
    }

}