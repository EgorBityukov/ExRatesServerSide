using ExRatesClassLibrary;

namespace ExRatesServerSide.Services.Interfaces
{
    public interface IExRatesCacheService
    {
        public Task<List<ExRate>> GetExRatesAsync(string currency,
                                                        DateTime startDate,
                                                        DateTime endDate);
    }
}
