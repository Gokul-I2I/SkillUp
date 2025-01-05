using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using SkillUpBackend.ViewModel;
using System.Text;

namespace SkillUp.Pages.BatchDashBoard
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
        public BatchCreateOrEdit BatchCreateOrEdit { get; set; }

        [BindProperty]
        public BatchViewModel BatchViewModel { get; set; }

        [BindProperty]
        public bool IsEditMode { get; set; }

        public async Task OnGet(int? id)
        {
            if (id.HasValue)
            {
                IsEditMode = true;
                var response = await _httpClient.GetAsync($"{_apiBaseUrl}/batch/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    BatchViewModel = JsonConvert.DeserializeObject<BatchViewModel>(content);
                }
            }
            else
            {
                IsEditMode = false;
            }
        }
        public async Task<IActionResult> OnPost(int? id)
        {
            var jsonContent = new StringContent(JsonConvert.SerializeObject(BatchCreateOrEdit), Encoding.UTF8, "application/json");
            if (id.HasValue) // Update existing Batch
            {
                var response = await _httpClient.PutAsync($"{_apiBaseUrl}/batch/{id}", jsonContent);
                if (!response.IsSuccessStatusCode)
                {
                    return Page();
                }
                return RedirectToPage("/BatchDashBoard/Index");
            }
            else // Create a new Batch
            {
                // Post the data to the API
                var response = await _httpClient.PostAsync($"{_apiBaseUrl}/batch", jsonContent);
                if (!response.IsSuccessStatusCode)
                {
                    return Page();
                }
                return RedirectToPage("/BatchDashBoard/Index");
            }
        }
    }
}
