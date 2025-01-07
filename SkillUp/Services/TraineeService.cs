using SkillUp.Commons;
using SkillUpBackend.Model;
using SkillUpBackend.ViewModel;

namespace SkillUp.Services
{
    public class TraineeService : ITraineeService
    {
        private readonly HttpClient _httpClient;

        public TraineeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<TopicViewModel>> GetAllTopicsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("https://localhost:7202/api/Trainee/Topic");
                response.EnsureSuccessStatusCode(); // Ensure response is successful (2xx)

                // Deserialize response content
                return await response.Content.ReadAsAsync<List<TopicViewModel>>();
            }
            catch (HttpRequestException ex)
            {
                // Log error or handle it (e.g., return an empty list)
                Console.Error.WriteLine($"Error fetching topics: {ex.Message}");
                return new List<TopicViewModel>(); // Return an empty list as fallback
            }
            catch (Exception ex)
            {
                // Handle other unexpected exceptions
                Console.Error.WriteLine($"Unexpected error: {ex.Message}");
                throw; // Rethrow or handle as necessary
            }
        }

        public async Task<List<SubTopicViewModel>> GetSubTopicsByTopicIdandEmailAsync(int id, string email)
        {
            try
            {
                var response = await _httpClient.GetAsync($"https://localhost:7202/api/Trainee/Topic/{id}/Email/{email}/SubTopic");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsAsync<List<SubTopicViewModel>>();
            }
            catch (HttpRequestException ex)
            {
                // Log error or handle it (e.g., return an empty list)
                Console.Error.WriteLine($"Error fetching subtopics: {ex.Message}");
                return new List<SubTopicViewModel>(); // Return an empty list as fallback
            }
            catch (Exception ex)
            {
                // Handle other unexpected exceptions
                Console.Error.WriteLine($"Unexpected error: {ex.Message}");
                throw; // Rethrow or handle as necessary
            }
        }

        public async Task<List<SubTaskViewModel>> GetSubTasksBySubTopicIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"https://localhost:7202/api/Trainee/SubTopic/{id}/SubTask");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsAsync<List<SubTaskViewModel>>();
            }
            catch (HttpRequestException ex)
            {
                // Log error or handle it (e.g., return an empty list)
                Console.Error.WriteLine($"Error fetching topics: {ex.Message}");
                return new List<SubTaskViewModel>(); // Return an empty list as fallback
            }
            catch (Exception ex)
            {
                // Handle other unexpected exceptions
                Console.Error.WriteLine($"Unexpected error: {ex.Message}");
                throw; // Rethrow or handle as necessary
            }
        }

        public async Task<bool> CreateSubTaskAsync(CreationSubTaskViewModel subTask)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("https://localhost:7202/api/Trainee/SubTask", subTask);
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {
                Console.Error.WriteLine($"Error creating SubTask: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateSubTaskAsync(CreationSubTaskViewModel subTask)
        {
            try
            {
                var patchData = new[]
                {
                    new PatchOperation { op = "replace", path = "/Title", value = subTask.Title },
                    new PatchOperation { op = "replace", path = "/Hours", value = subTask.Hours },
                    new PatchOperation { op = "replace", path = "/Minutes", value = subTask.Minutes }
                };

                var response = await _httpClient.PatchAsJsonAsync($"https://localhost:7202/api/Trainee/SubTask/{subTask.Id}", patchData);
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {
                Console.Error.WriteLine($"Error updating subtask: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteSubTaskAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"https://localhost:7202/api/Trainee/SubTask/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {
                Console.Error.WriteLine($"Error deleting subtask: {ex.Message}");
                return false;
            }
        }

        public async Task<List<UserSubtopicViewModel>> GetUserSubtopicByEmailAsync(string email) 
        {

            try
            {
                var response = await _httpClient.GetAsync($"https://localhost:7202/api/Trainee/UserSubtopic/{email}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsAsync<List<UserSubtopicViewModel>>();
            }
            catch (HttpRequestException ex)
            {
                // Log error or handle it (e.g., return an empty list)
                Console.Error.WriteLine($"Error fetching topics: {ex.Message}");
                return new List<UserSubtopicViewModel>(); // Return an empty list as fallback
            }
            catch (Exception ex)
            {
                // Handle other unexpected exceptions
                Console.Error.WriteLine($"Unexpected error: {ex.Message}");
                throw; // Rethrow or handle as necessary
            }
        }
    }
}
