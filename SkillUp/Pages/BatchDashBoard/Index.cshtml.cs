using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using SkillUpBackend.Model;

namespace SkillUp.Pages.BatchDashBoard
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;

        public IEnumerable<Batch> Batch { get; set; }
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
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<Batch>>>(content);
                    Batch = apiResponse?.Result;
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
        }
    }
}
