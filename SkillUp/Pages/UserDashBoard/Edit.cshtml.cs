using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using SkillUpBackend.Model;
using System.Text;

namespace SkillUp.Pages.UserDashBoard
{
    public class EditModel : PageModel
    {
        private HttpClient _httpClient;
        public EditModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        [BindProperty]
        public UserEditModel User { get; set; }
        public async Task OnGet(int id)
        {
            var response = await _httpClient.GetAsync($"Https://localhost:7202/api/user/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                User = JsonConvert.DeserializeObject<UserEditModel>(content);
            }
        }
        public async Task<IActionResult> OnPost(int id)
        {
            var jsonContent = new StringContent(JsonConvert.SerializeObject(User), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"https://localhost:7202/api/user/{id}", jsonContent);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/UserDashBoard/Index");
            }
            return Page();
        }
    }
}