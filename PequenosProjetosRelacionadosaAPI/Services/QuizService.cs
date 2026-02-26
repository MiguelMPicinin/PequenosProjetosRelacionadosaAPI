using PequenosProjetosRelacionadosaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PequenosProjetosRelacionadosaAPI.Services
{
    public class QuizService : IQuizService
    {
        private readonly HttpClient _httpClient;

        public QuizService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<QuizQuestion>> GetQuestionsAsync(int amount)
        {
            var response = await _httpClient.GetAsync($"/api/quiz?amount={amount}");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<QuizQuestion>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<QuizResult> CheckAnswersAsync(List<QuizAnswer> answers)
        {
            var content = new StringContent(JsonSerializer.Serialize(answers), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/quiz/check", content);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<QuizResult>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}
