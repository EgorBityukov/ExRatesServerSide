using ExRatesClassLibrary;

namespace ExRatesServerSide.Services.Interfaces
{
    public interface ISerializeExRatesService
    {
        public Task SerializeRatesToFileAsync(HashSet<ExRate> serExRates, string path);
        public Task<HashSet<ExRate>> DeserializeRatesFromFileAsync(string path);
    }
}
