using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkillUp.Services;
using SkillUpBackend.Model;

namespace SkillUp.Pages.Mentor
{
    public class ManageSubtopicModel : PageModel
    {
        private readonly ApiService _apiService;

        public ManageSubtopicModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        public List<Subtopic> Subtopics { get; set; } = new();

        [BindProperty]
        public Subtopic Subtopic { get; set; }

        public async Task OnGetAsync()
        {
            Subtopics = await _apiService.GetSubtopicsAsync();
        }

        public async Task<IActionResult> OnPostAddAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _apiService.AddSubtopicAsync(Subtopic);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await _apiService.DeleteSubtopicAsync(id);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostEditAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _apiService.EditSubtopicAsync(Subtopic);
            return RedirectToPage();
        }
    }
}
