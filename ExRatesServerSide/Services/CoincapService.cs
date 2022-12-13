using ExRatesClassLibrary;
using ExRatesServerSide.Services.Interfaces;

namespace ExRatesServerSide.Services
{
    public class CoincapService : ICoincapService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _httpClient;

        public CoincapService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _httpClient = _httpClientFactory.CreateClient("coincapClient");
        }

        public async Task<List<ExRate>> GetRangeOfExRateAsync(string currency, DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }
    }
}
