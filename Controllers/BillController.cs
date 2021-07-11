using Microsoft.AspNetCore.Mvc;
using BookStore.Repository;
using Microsoft.Extensions.Logging;
using BookStore.ActionModels;
using BookStore.DTO;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class BillController: ControllerBase
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

            return Ok(result.Select(bill => new BillDTO(bill)));
        }

        [HttpPost]
        public IActionResult AddNewBill([FromBody] AddBillModel model)
        {
            var result = _repository.CreateBill(model);
            return Ok(new BillDTO(result) );
        }

        [Route("{id}")]
        public IActionResult GetSingleBill(int id)
        {
            var result = _repository.GetSingleBill(id);
            if(result == null)
                return NotFound();
            return Ok( new BillDTO(result)); 
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

            var resultDTO = result.Select(billDe=> new BillDetailDTO(billDe));

            return Ok(resultDTO);

        }

        [HttpPost]
        [Route("detail/{billId}")]
        public IActionResult AddBookEntryToBillDetail(int billId,[FromBody] List<AddBookToBill> model)
        {
            var result = _repository.BulkAddBookToBill(billId, model);

            if(result == null)
                return NotFound();
            return Ok();
        }

        [HttpPost]
        [Route("detail/update/{billID}")]
        public IActionResult UpdateBillDetailEntries(int billID, [FromBody] List<AddBookToBill> model)
        {
            var result = _repository.BulkUpdateAllBillDetailEntry(billID,model); 

            if(result == null)
                return BadRequest();
            return Ok(result.Select(billDetail => new BillDetailDTO(billDetail)).ToList());
        }

        [HttpPost]
        [Route("detail/delete/{billDetailID}")]
        public IActionResult DeleteBillDetailEntry(int billDetailID)
        {
            var result = _repository.DeleteBillDetailEntry(billDetailID);

            if(result == null)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}