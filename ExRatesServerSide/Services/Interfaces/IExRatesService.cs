using ExRatesClassLibrary;

namespace ExRatesServerSide.Services.Interfaces
{
    public interface IExRatesService
    {
        public Task<List<ExRate>> GetExRatesByCurrencyAsync(string currency, DateTime startDate, DateTime endDate);
    }
}
