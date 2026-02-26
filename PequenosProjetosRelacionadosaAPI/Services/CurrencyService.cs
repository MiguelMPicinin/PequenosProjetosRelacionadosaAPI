using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using PequenosProjetosRelacionadosaAPI.Models;

namespace PequenosProjetosRelacionadosaAPI.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly HttpClient _httpClient;

        public CurrencyService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ConversionResult> ConvertAsync(ConversionRequest request)
        {
            string url = $"/api/currency/convert?from={request.From}&to={request.To}&amount={request.Amount}";
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<ConversionResult>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Erro na conversão: {error}");
            }
        }
    }
}