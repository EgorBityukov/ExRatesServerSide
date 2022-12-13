using ExRatesClassLibrary;
using ExRatesServerSide.Services.Interfaces;
using System.Text.Json;

namespace ExRatesServerSide.Services
{
    public class SerializeExRatesService : ISerializeExRatesService
    {
        public async Task SerializeRatesToFileAsync(List<ExRate> serExRates, string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                await JsonSerializer.SerializeAsync(fs, serExRates);
            }
        }

        public async Task<List<ExRate>> DeserializeRatesFromFileAsync(string path)
        {
            List<ExRate> deserExRates = null;

            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                if (fs.Length > 0)
                {
                    deserExRates = await JsonSerializer.DeserializeAsync<List<ExRate>>(fs);
                }
            }

            return deserExRates;
        }
    }
}
