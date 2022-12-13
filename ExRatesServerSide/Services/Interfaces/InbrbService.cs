using ExRatesClassLibrary;
using ExRatesServerSide.Models.ResponseModels;

namespace ExRatesServerSide.Services.Interfaces
{
    public interface InbrbService
    {
        public Task<List<RateShort>> GetRangeOfExRateAsync(string currency, DateTime startDate, DateTime endDate);
    }
}
