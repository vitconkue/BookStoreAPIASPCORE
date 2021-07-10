using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookStore.Repository;
using System.Linq;
using BookStore.DTO;
using BookStore.ActionModels;
using BookStore.Services;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        
        

        public ReportController(IReportService reportService)
        {
         
            _reportService = reportService; 
        }
        
        [HttpGet]
        [Route("{books-report}")]
        public IActionResult GetBookAmountReport(int month, int year)
        {
            var records = _reportService.GetBooksReportRecords(month,year); 
            return Ok(records); 
        }
    }
}