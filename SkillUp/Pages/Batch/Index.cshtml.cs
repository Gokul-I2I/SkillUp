using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using SkillUpBackend.Model;
using SkillUpBackend.ViewModel;

namespace SkillUp.Pages.Batch
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;

        public ICollection<BatchViewModel> BatchViewModels { get; set; }
        public IndexModel(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient("MyHttpClient");
            _apiBaseUrl = configuration["ApiBaseUrl"];

        }
        public async void OnGet()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_apiBaseUrl}/batch");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<BatchViewModel>>>(content);
                    BatchViewModels = apiResponse.Result;
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }
    }
}
