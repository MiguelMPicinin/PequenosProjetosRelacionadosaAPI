using PequenosProjetosRelacionadosaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace PequenosProjetosRelacionadosaAPI.Services
{
    public class RAWGService : IRAWGService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "770f348027a64dfbb4cc1c95cb4a9766"; // substitua

        public RAWGService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Game>> SearchGamesAsync(string query)
        {
            string url = $"https://api.rawg.io/api/games?key={_apiKey}&search={Uri.EscapeDataString(query)}&page_size=5";
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                using JsonDocument doc = JsonDocument.Parse(json);
                var results = doc.RootElement.GetProperty("results").EnumerateArray();

                var games = new List<Game>();
                foreach (var game in results)
                {
                    var platforms = game.GetProperty("platforms").EnumerateArray()
                                        .Select(p => p.GetProperty("platform").GetProperty("name").GetString())
                                        .ToList();

                    string metacritic = "N/A";
                    if (game.TryGetProperty("metacritic", out var metaElem))
                    {
                        if (metaElem.ValueKind == JsonValueKind.Number)
                            metacritic = metaElem.GetInt32().ToString();
                        // se for null, permanece "N/A"
                    }

                    games.Add(new Game
                    {
                        Name = game.GetProperty("name").GetString(),
                        Released = game.TryGetProperty("released", out var rel) ? rel.GetString() : "N/A",
                        Metacritic = metacritic,
                        Platforms = platforms
                    });
                }
                return games;
            }
            else
            {
                throw new Exception("Erro ao buscar jogos na RAWG.");
            }
        }
    }
}