using Microsoft.AspNetCore.Mvc;
using BookStore.Models;
using BookStore.Repository;
using Microsoft.Extensions.Logging;
using BookStore.Errors;
using BookStore.ActionModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using BookStore.DTO;

namespace BookStore.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class BillController: Controller
    {
        private readonly IBillRepository _repository;

        private readonly ILogger<BillController> _logger;

        public BillController(IBillRepository repository, ILogger<BillController> logger)
        {
            _repository = repository;
            _logger = logger;
        }


        public IActionResult GetAllBills()
        {
            var result = _repository.GetAllBills();

            if(result.Count == 0)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddNewBill([FromBody] AddBillModel model)
        {
            var result = _repository.CreateBill(model);
            return Ok(result);
        }

        [Route("{id}")]
        public IActionResult GetSingleBill(int id)
        {
            var result = _repository.GetSingleBill(id);
            if(result == null)
                return NotFound();
            return Ok( BillDTO.GetDTO(result)); 
        }

        [HttpPost]
        [Route("delete/{id}")]
        public IActionResult DeleteBill(int id)
        {
            var result = _repository.DeleteBill(id);
            if(result == null)
                return NotFound();
            return Ok(new {Delete = $"Deleted bill {id}"});
        }

        // detail endpoints

        [Route("detail/{id}")]
        public IActionResult GetBillDetail(int id)
        {
            var result = _repository.GetSingleBillDetail(id);
            if(result ==null)
            {
                return NotFound();
            }

            return Ok(result);

        }
    }
}