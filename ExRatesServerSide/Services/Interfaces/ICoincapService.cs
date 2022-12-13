using ExRatesClassLibrary;

namespace ExRatesServerSide.Services.Interfaces
{
    public interface ICoincapService
    {
        public Task<List<ExRate>> GetRangeOfExRateAsync(string currency, DateTime startDate, DateTime endDate);
    }
}
