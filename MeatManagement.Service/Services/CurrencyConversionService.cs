using MeatManager.Service.Interfaces;
using MeatManager.Service.Models;
using System.Net.Http.Json;

namespace MeatManager.Service.Services
{
    public class CurrencyConversionService : ICurrencyConversionService
    {
        private readonly HttpClient httpClient;
        private readonly Dictionary<string, CurrencyRate> cache = new();

        public CurrencyConversionService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<decimal> ConvertToBRLAsync(decimal amount, string currencyCode)
        {
            if (currencyCode == "BRL") return amount;

            if (!cache.TryGetValue(currencyCode, out var rate) || (DateTime.UtcNow - rate.LastUpdated).TotalHours > 24)
            {
                var response = await httpClient.GetFromJsonAsync<Dictionary<string, AwesomeApiResponse>>(
                    $"https://economia.awesomeapi.com.br/json/last/{currencyCode}-BRL");

                if (response == null) throw new Exception("Error fetching exchange rate.");

                var rateInfo = response[$"{currencyCode}BRL"];
                rate = new CurrencyRate
                {
                    Code = currencyCode,
                    Rate = rateInfo.Bid,
                    LastUpdated = DateTime.UtcNow
                };

                cache[currencyCode] = rate;
            }

            return amount * rate.Rate;
        }
    }

}
