using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using SkillUpBackend.Model;
using System.Text;

namespace SkillUp.Pages.Login
{
    public class LoginModel : PageModel
    {
        private HttpClient _httpClient;
        private readonly string _apiBaseUrl;

        public LoginModel(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiBaseUrl = configuration["ApiBaseUrl"];

        }
        [BindProperty]
        public LoginRequest LoginRequest { get; set; }

        public async Task<IActionResult> OnPost()
        {
            var jsonContent = new StringContent(JsonConvert.SerializeObject(LoginRequest), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_apiBaseUrl}/login", jsonContent);
            if (response.IsSuccessStatusCode)
            {

                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<User>(responseContent);
                if (result != null && result.Role != null)
                {
                    switch (result.Role.Name)
                    {
                        case "admin":
                            return RedirectToPage("/AdminDashBoard/Admin");
                        case "mentor":
                            return RedirectToPage("/Mentor/Index");
                        case "trainee":
                            return RedirectToPage("/TraineeDashBoard/Trainee");
                        default:
                            break;
                    }
                }
            }
            if (!response.IsSuccessStatusCode)
            {
                return Page();
            }
            return Page();
        }
    }
}