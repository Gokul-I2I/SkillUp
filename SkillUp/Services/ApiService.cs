using SkillUpBackend.Model;
using System.Net.Http.Json;

namespace SkillUp.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Subtopic>> GetSubtopicsAsync()
        {
            var response = await _httpClient.GetAsync("api/mentor/subtopics");

            // Ensure the request was successful
            response.EnsureSuccessStatusCode();

            // Deserialize the response into SubtopicResponse
            var result = await response.Content.ReadFromJsonAsync<SubtopicResponse>();

            // Return the list of subtopics, or an empty list if null
            return result?.Subtopics ?? new List<Subtopic>();
        }


        public async Task<HttpResponseMessage> AddSubtopicAsync(Subtopic subtopic)
        {
            return await _httpClient.PostAsJsonAsync("api/mentor/subtopic", subtopic);
        }

        public async Task<HttpResponseMessage> EditSubtopicAsync(Subtopic subtopic)
        {
            return await _httpClient.PutAsJsonAsync("api/mentor/subtopic", subtopic);
        }

        public async Task<HttpResponseMessage> DeleteSubtopicAsync(int id)
        {
            return await _httpClient.DeleteAsync($"api/mentor/subtopic/{id}");
        }
    }
}
