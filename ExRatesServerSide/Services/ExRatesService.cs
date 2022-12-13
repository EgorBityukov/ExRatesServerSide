using ExRatesClassLibrary;
using ExRatesServerSide.Services.Interfaces;

namespace ExRatesServerSide.Services
{
    public class ExRatesService : IExRatesService
    {
        private readonly ICoincapService _coincapService;
        private readonly InbrbService _nbrbService;

        public ExRatesService(ICoincapService coincapService,
                              InbrbService nbrbService)
        {
            _coincapService = coincapService;
            _nbrbService = nbrbService;
        }

        public async Task<List<ExRate>> GetExRatesByCurrencyAsync(string currency, DateTime startDate, DateTime endDate)
        {
            List<ExRate> exRates = null;

            if (currency.Equals("BTC"))
            {
                var exRatesResult = await _coincapService.GetRangeOfExRateAsync(currency, startDate, endDate);
            }
            else
            {
                var exRatesResult = await _nbrbService.GetRangeOfExRateAsync(currency, startDate, endDate);
            }

            return exRates;
        }
    }
}
