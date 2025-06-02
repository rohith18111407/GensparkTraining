using System.Text;
using System.Text.Json;
using MoodJournal.Interfaces;

namespace MoodJournal.Services
{
    public class LLMService : ILLMService
    {
        private readonly HttpClient _httpClient;

        public LLMService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GenerateTipAsync(string mood, string note)
        {
            var request = new
            {
                model = "mistral",
                prompt = $"Mood: {mood}. Note: {note}. Suggest a positive self-care tip.",
                stream = false
            };

            var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("", content);
            response.EnsureSuccessStatusCode();

            var result = await JsonSerializer.DeserializeAsync<Dictionary<string, object>>(await response.Content.ReadAsStreamAsync());
            return result?["response"]?.ToString() ?? "Relax, we'll get you what you need";
        }
    }
}