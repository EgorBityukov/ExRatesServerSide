using ExRatesClassLibrary;
using ExRatesServerSide.Models.ResponseModels;

namespace ExRatesServerSide.Services.Interfaces
{
    public interface InbrbService
    {
        public Task<List<RateShort>> GetRangeOfExRateAsync(int currency, DateTime startDate, DateTime endDate);
        public Task<List<Currency>> GetCurrenciesAsync();
    }
}
