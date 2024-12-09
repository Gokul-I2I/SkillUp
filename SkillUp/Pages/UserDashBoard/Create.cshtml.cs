using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SkillUpBackend.Model;
using System.Text;

namespace SkillUp.Pages.UserDashBoard
{
    public class CreateModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public CreateModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        

        [BindProperty]
        public UserCreateModel User { get; set; }
        public async Task OnGetAsync()
        {
            await LoadRoles();
        }

        private async Task LoadRoles()
        {
            var response = await _httpClient.GetAsync("https://localhost:7202/api/roles");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var jsonObject = JsonConvert.DeserializeObject<JObject>(content);
                var roles = jsonObject["result"].ToObject<IEnumerable<Role>>();
                ViewData["Roles"] = roles.Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Id.ToString()
                }).ToList();
            }
            else
            {
                ViewData["Roles"] = new List<SelectListItem>();
            }
        }

        public async Task<IActionResult> OnPost()
        {
            // Serialize the input model to JSON
            var jsonContent = new StringContent(JsonConvert.SerializeObject(User), Encoding.UTF8, "application/json");

            // Post the data to the API
            var response = await _httpClient.PostAsync("https://localhost:7202/api/user", jsonContent);
            if (!response.IsSuccessStatusCode)
            {
            await LoadRoles();
                return Page();
            }
            await LoadRoles();
            return RedirectToPage("/UserDashBoard/Index");
        }
    }
}
