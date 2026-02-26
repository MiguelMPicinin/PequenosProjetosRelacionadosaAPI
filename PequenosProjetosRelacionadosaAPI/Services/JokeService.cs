using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace PequenosProjetosRelacionadosaAPI.Services
{
    public class JokeService : IJokeService
    {
        private readonly HttpClient _httpClient;

        public JokeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetRandomJokeAsync()
        {
            var response = await _httpClient.GetAsync("/api/jokes/random");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            using JsonDocument doc = JsonDocument.Parse(json);
            return doc.RootElement.GetProperty("joke").GetString();
        }

        public async Task<List<string>> GetAllJokesAsync()   // nome corrigido
        {
            var response = await _httpClient.GetAsync("/api/jokes");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<string>>(json);
        }

        public async Task<int> GetJokeCountAsync()
        {
            var response = await _httpClient.GetAsync("/api/jokes/count");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            using JsonDocument doc = JsonDocument.Parse(json);
            return doc.RootElement.GetProperty("count").GetInt32();
        }
    }
}