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

        public async Task<Subtopic?> GetSubtopicByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/mentor/subtopic/{id}");

            // Ensure the request was successful
            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<Subtopic>();
        }

        public async Task<bool> ChangeSubtopicStateAsync(int id, string newState, string updatedByRole)
        {
            // Construct the request payload
            var stateChangeRequest = new
            {
                SubtopicId = id,
                NewState = newState,
                UpdatedBy = updatedByRole
            };

            // Send the request to the API endpoint
            var response = await _httpClient.PutAsJsonAsync("api/mentor/subtopic/state", stateChangeRequest);

            return response.IsSuccessStatusCode;
        }
        public async Task<List<UserSubtopic>> GetUserSubtopicsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/mentor/usersubtopics");
                Console.WriteLine($"UserSubtopics response: {await response.Content.ReadAsStringAsync()}");


                // First check if the request was successful
                if (!response.IsSuccessStatusCode)
                {
                    // Log or handle the error response
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error response: {errorContent}");
                    return new List<UserSubtopic>();
                }

                var wrapper = await response.Content.ReadFromJsonAsync<UserSubtopicsResponse>();
                return wrapper?.UserSubtopics ?? new List<UserSubtopic>();
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Exception in GetUserSubtopicsAsync: {ex}");
                throw;
            }
        }

        public async Task<bool> UpdateUserSubtopicStateAsync(int userId, int subtopicId, TaskState state)
        {
            var request = new
            {
                UserId = userId,
                SubtopicId = subtopicId,
                State = state
            };

            var response = await _httpClient.PutAsJsonAsync("api/mentor/usersubtopic/state", request);
            return response.IsSuccessStatusCode;
        }
    }
}

    
