using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkillUp.Services;
using SkillUpBackend.Model; // Ensure ApiService is available here

namespace SkillUp.Pages.Mentor
{
    public class IndexModel : PageModel
    {
        private readonly ApiService _apiService;

        // Constructor to inject ApiService
        public IndexModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        // Property to store list of topics
        public List<Topic> Topics { get; set; }
        public IActionResult OnPostAddTopic()
        {
            // Your logic for adding a topic
            return RedirectToPage(); // Refresh the page or redirect as needed
        }

        // OnGet method to load topics when the page is requested
        //public async Task OnGetAsync()
        //{
        //    Topics = await _apiService.GetTopicsAsync();
        //}
    }
}
