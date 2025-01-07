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
        public LoginModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        [BindProperty]
        public LoginRequest LoginRequest { get; set; }
        public string ErrorMessage { get; set; }


        public async Task<IActionResult> OnPost()
        {
            var jsonContent = new StringContent(JsonConvert.SerializeObject(LoginRequest), Encoding.UTF8, "application/json");
            
            var response = await _httpClient.PostAsync("https://localhost:7202/api/login", jsonContent);
            if (response.IsSuccessStatusCode)
            {

                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<User>(responseContent);
                Response.Cookies.Append("UserEmail", result.Email, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = false,
                    SameSite = SameSiteMode.Strict
                });
                Response.Cookies.Append("UserId", result.Id.ToString(), new CookieOptions
                {
                    HttpOnly = true,
                    Secure = false,
                    SameSite = SameSiteMode.Strict
                });

                if (result != null && result.Role != null)
                {
                    switch (result.Role.Name)
                    {
                        case "admin":
                            return RedirectToPage("/AdminDashBoard/Admin");
                        case "mentor":
                            return RedirectToPage("/Mentor/Index");
                        case "trainee":
                            return RedirectToPage("/TraineeDashBoard/Index");
                        default:
                            break;
                    }
                }
            }
            ErrorMessage = "Invalid email or password. Please try again.";
            return Page();
        }
    }
}
