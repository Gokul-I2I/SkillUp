using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SkillUpBackend.Model;
using SkillUpBackend.ViewModel;
using System.Text;

namespace SkillUp.Pages.UserDashBoard
{
    public class CreateOrEditModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;

        public CreateOrEditModel(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiBaseUrl = configuration["ApiBaseUrl"];
        }

        [BindProperty]
        public UserCreateOrEdit UserCreateOrEdit { get; set; }

        [BindProperty]
        public bool IsEditMode { get; set; }

        public async Task OnGet(int? id)
        {
            await LoadRoles();

            if (id.HasValue)
            {
                IsEditMode = true;
                var response = await _httpClient.GetAsync($"{_apiBaseUrl}/user/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    UserCreateOrEdit = JsonConvert.DeserializeObject<UserCreateOrEdit>(content);
                }
            }
            else
            {
                IsEditMode = false;
            }
        }
        private async Task LoadRoles()
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/roles");
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

        public async Task<IActionResult> OnPost(int? id)
        {
            var jsonContent = new StringContent(JsonConvert.SerializeObject(UserCreateOrEdit), Encoding.UTF8, "application/json");
            if (id.HasValue) // Update existing user
            {
                var response = await _httpClient.PutAsync($"{_apiBaseUrl}/user/{id}", jsonContent);
                if (!response.IsSuccessStatusCode)
                {
                    await LoadRoles();
                    return Page();
                }
                await LoadRoles();
                return RedirectToPage("/UserDashBoard/Index");
            }
            else // Create a new user
            {
                // Post the data to the API
                var response = await _httpClient.PostAsync($"{_apiBaseUrl}/user", jsonContent);
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
}