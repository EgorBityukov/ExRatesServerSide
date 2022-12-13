using ExRatesClassLibrary;
using ExRatesServerSide.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace ExRatesServerSide.Services
{
    public class ExRatesCacheService : IExRatesCacheService
    {
        private readonly IExRatesService _exRatesService;
        private readonly ISerializeExRatesService _serializeExRatesService;
        private readonly IMemoryCache _memoryCache;
        private readonly IConfiguration _config;

        private List<ExRate> memoryExRates = null;

        public ExRatesCacheService(IExRatesService exRatesService,
                                   ISerializeExRatesService serializeExRatesService,
                                   IMemoryCache memoryCache,
                                   IConfiguration config)
        {
            _exRatesService = exRatesService;
            _serializeExRatesService = serializeExRatesService;
            _memoryCache = memoryCache;
            _config = config;
        }

        public async Task<List<ExRate>> GetExRatesAsync(string currency,
                                                        DateTime startDate,
                                                        DateTime endDate)
        {
            await ConfigureCacheAsync();

            List<ExRate> exRates = new List<ExRate>();

            if (memoryExRates != null)
            {
                if (memoryExRates.Where(r => r.Currency == currency && r.Date >= startDate && r.Date <= endDate).Any())
                {
                    exRates = memoryExRates.Where(r => r.Currency == currency && r.Date >= startDate && r.Date <= endDate).ToList();
                }
            }
            else
            {
                await _exRatesService.GetExRatesByCurrencyAsync(currency, startDate, endDate);
            }

            return exRates;
        }

        private async Task ConfigureCacheAsync()
        {
            if (memoryExRates == null)
            {
                await LoadCacheFromFileAsync();
            }

            memoryExRates = GetExRatesFromCache();
        }

        private async Task LoadCacheFromFileAsync()
        {
            memoryExRates = await _serializeExRatesService.DeserializeRatesFromFileAsync(_config.GetValue<string>("PathSerializer"));
            AddExRatesToCache(memoryExRates);
        }

        private async Task UpdateCache(List<ExRate> updExRates)
        {
            await _serializeExRatesService.SerializeRatesToFileAsync(updExRates, _config.GetValue<string>("PathSerializer"));
            AddExRatesToCache(updExRates);
        }

        private List<ExRate> GetExRatesFromCache()
        {
            List<ExRate> exRates = null;
            _memoryCache.TryGetValue("exRates", out exRates);
            return exRates;
        }

        private void AddExRatesToCache(List<ExRate> exRates)
        {
            _memoryCache.Set("exRates", exRates, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(3)));
        }
    }
}
