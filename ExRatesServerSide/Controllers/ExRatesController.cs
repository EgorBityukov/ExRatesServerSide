using Microsoft.AspNetCore.Mvc;

namespace ExRatesServerSide.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExRatesController : ControllerBase
    {
        private readonly ILogger<ExRatesController> _logger;

        public ExRatesController(ILogger<ExRatesController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetExRates")]
        public void Get()
        {
            
        }
    }
}