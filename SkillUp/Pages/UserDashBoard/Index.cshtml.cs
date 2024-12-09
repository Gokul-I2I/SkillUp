using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using SkillUpBackend.Model;

namespace SkillUp.Pages.UserDashBoard
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public IEnumerable<User> Users { get; set; }

        public IndexModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // OnGetAsync method fetches user data from the API
        public async Task OnGetAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("https://localhost:7202/api/user");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<User>>>(content);
                    Users = apiResponse.Result;
                }
            }
            catch (Exception ex)
            {
                Users = new List<User>();
                Console.WriteLine(ex.Message);
            }
        }
    }
}
