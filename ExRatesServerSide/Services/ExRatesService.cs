using AutoMapper;
using ExRatesClassLibrary;
using ExRatesServerSide.Services.Interfaces;

namespace ExRatesServerSide.Services
{
    public class ExRatesService : IExRatesService
    {
        private readonly ICoincapService _coincapService;
        private readonly InbrbService _nbrbService;
        private readonly IMapper _mapper;

        public ExRatesService(ICoincapService coincapService,
                              InbrbService nbrbService,
                              IMapper mapper)
        {
            _coincapService = coincapService;
            _nbrbService = nbrbService;
            _mapper = mapper;
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
                var currencies = await _nbrbService.GetCurrenciesAsync();
                var currencyObj = currencies.Where(c => c.Cur_Abbreviation.Equals(currency) && c.Cur_DateEnd >= DateTime.UtcNow).LastOrDefault();
                var exRatesResult = await _nbrbService.GetRangeOfExRateAsync(currencyObj.Cur_ID, startDate, endDate);
                exRates = _mapper.Map<List<ExRate>>(exRatesResult);
                exRates.ForEach(r => 
                { 
                    r.Currency = currencyObj.Cur_Abbreviation; 
                    r.Amount = currencyObj.Cur_Scale; 
                });
            }

            return exRates;
        }
    }
}
