using ExRatesClassLibrary;
using ExRatesServerSide.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ExRatesServerSide.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExRatesController : ControllerBase
    {
        private readonly ILogger<ExRatesController> _logger;
        private readonly IExRatesCacheService _exRatesCacheService;

        public ExRatesController(ILogger<ExRatesController> logger,
                                 IExRatesCacheService exRatesCacheService)
        {
            _logger = logger;
            _exRatesCacheService = exRatesCacheService;
        }

        [HttpGet(Name = "GetExRates")]
        public async Task<ActionResult<List<ExRate>>> Get(string currency, 
                                                          DateTime startDate, 
                                                          DateTime endDate)
        {
            try
            {
                var res = await _exRatesCacheService.GetExRatesAsync(currency, startDate, endDate);
            
                if (res != null && res.Count > 0)
                {
                    return Ok(res);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch
            {
                return new StatusCodeResult(500);
            }
        }
    }
}