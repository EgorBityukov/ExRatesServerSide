using ExRatesClassLibrary;
using ExRatesServerSide.Models.ResponseModels;
using ExRatesServerSide.Services.Interfaces;
using Microsoft.AspNetCore.WebUtilities;
using System.Text.Json;

namespace ExRatesServerSide.Services
{
    public class nbrbService : InbrbService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _httpClient;

        public nbrbService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _httpClient = _httpClientFactory.CreateClient("nbrbClient");
        }

        public async Task<List<RateShort>> GetRangeOfExRateAsync(string currencyId, DateTime startDate, DateTime endDate)
        {
            var parameters = new Dictionary<string, string>()
            {
                ["startDate"] = startDate.ToString("yyyy-MM-dd"),
                ["endDate"] = endDate.ToString("yyyy-MM-dd")
            };

            var requestUrl = QueryHelpers.AddQueryString($"rates/dynamics/{currencyId}", parameters);

            HttpResponseMessage response = await _httpClient.GetAsync(requestUrl);
            string responseBody = await response.Content.ReadAsStringAsync();
            var res = JsonSerializer.Deserialize<List<RateShort>>(responseBody);
            return res;
        }
    }
}
