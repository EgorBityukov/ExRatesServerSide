using ExRatesClassLibrary;

namespace ExRatesServerSide.Services.Interfaces
{
    public interface ISerializeExRatesService
    {
        public Task SerializeRatesToFileAsync(List<ExRate> serExRates, string path);
        public Task<List<ExRate>> DeserializeRatesFromFileAsync(string path);
    }
}
