using PequenosProjetosRelacionadosaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace PequenosProjetosRelacionadosaAPI.Services
{
    public class PokemonService : IPokemonServices   // confira o nome da interface
    {
        private readonly HttpClient _httpClient;

        public PokemonService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Pokemon> GetPokemonAsync(string name)
        {
            string url = $"https://pokeapi.co/api/v2/pokemon/{name.ToLower()}";
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                using JsonDocument doc = JsonDocument.Parse(json);
                var root = doc.RootElement;

                return new Pokemon
                {
                    Name = root.GetProperty("name").GetString(),
                    // Corrigido: não usar JsonSerializer.Deserialize extra
                    Types = root.GetProperty("types").EnumerateArray()
                                .Select(t => t.GetProperty("type").GetProperty("name").GetString())
                                .ToList(),
                    Abilities = root.GetProperty("abilities").EnumerateArray()
                                .Select(a => a.GetProperty("ability").GetProperty("name").GetString())
                                .ToList(),
                    Stats = root.GetProperty("stats").EnumerateArray()
                                .ToDictionary(
                                    s => s.GetProperty("stat").GetProperty("name").GetString(),
                                    s => s.GetProperty("base_stat").GetInt32()
                                )
                };
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                throw new Exception("Pokémon não encontrado.");
            else
                throw new Exception("Erro na comunicação com a PokéAPI.");
        }
    }
}