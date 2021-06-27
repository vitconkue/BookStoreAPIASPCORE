using Microsoft.AspNetCore.Mvc;
using BookStore.Repository;
using Microsoft.Extensions.Logging;
using BookStore.ActionModels;
using BookStore.Models;
using BookStore.DTO;
using System.Linq;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class CustomerController: ControllerBase{
        private readonly ILogger<CustomerController> _logger;
        private readonly ICustomerRepository _repository;

        public CustomerController(ILogger<CustomerController> logger, ICustomerRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }


        public IActionResult GetAllCustomer()
        {
            var result = _repository.GetAllCustomers();
            if(result.Count == 0)
            {
                return NotFound();
            }
            return Ok(result.Select(obj => new CustomerDTO(obj)));
        }

        [Route("{id}")]
        public IActionResult GetSingleCustomer(int id)
        {
            var found = _repository.GetSingleCustomer( id);
            if(found == null)
                return NotFound();
            return Ok(new CustomerDTO(found));
        }

        [Route("debt/{id}")]
        public IActionResult GetSingleCustomerDebt(int id)
        {
            var result = _repository.CalculateTotalDebt( id);
            if(result == null)
                return BadRequest();
            return Ok(result);
        }


        [HttpPost]
        public IActionResult AddCustomer([FromBody] AddCustomerModel model)
        {
            Customer newCustomer = _repository.AddCustomer(model);

            if(newCustomer == null)
            {
                return BadRequest(new {Error = "Failed adding customer, contact An"}); 
            }
            return Ok( new CustomerDTO(newCustomer));
        }
    }
}