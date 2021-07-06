
using Microsoft.AspNetCore.Mvc;
using BookStore.Repository;
using Microsoft.Extensions.Logging;


namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationsController: ControllerBase
    {
        private readonly ILogger<ConfigurationsController> _logger;
        private readonly IConfigurationRepository _repository;

        public ConfigurationsController(ILogger<ConfigurationsController> logger, IConfigurationRepository repository)
        {
            _logger = logger;
            _repository = repository; 
        }
        public IActionResult GetAll()
        {
            _logger.LogInformation("A client trying to get all configs"); 
            var result = _repository.GetAllConfigurations();
             _logger.LogInformation($"Returned {result.Count} config(s)"); 

            if(result.Count == 0)
            {
                return NotFound();
            }
            return Ok(result); 
        }
        [Route("{name}")]
        public IActionResult GetSingleConfiguration(string name)
        {
            var result = _repository.GetSingleConfiguration(name);
            if(result == null)
            {
                return NotFound(); 
            }

            return Ok(result);
        }

        [HttpPost]
        [Route("{name}/{value}")]

        public IActionResult ChangeConfiguration(string name, int value )
        {
            var result = _repository.ChangeConfiguration(name,value);
            if(result == null)
                return NotFound();
            return Ok(result);
        }

        [Route("toggle/{name}")]
        [HttpPost]
        public IActionResult toggleConfiguration(string name)
        {
            var result = _repository.ToggleConfigurationStatus(name);
            if(result == null) 
                return NotFound();

            return Ok(result);
        }
    }
}