using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using SkillUpBackend.Model;
using SkillUp.Services;
using System.Linq;
using System.Threading.Tasks;

namespace SkillUp.Pages.Mentor
{
    public class IndexModel : PageModel
    {
        private readonly ApiService _apiService;

        public IndexModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        [BindProperty]
        public List<UserSubtopic> UserSubtopics { get; set; }

        [BindProperty]
        public List<Topic> Topics { get; set; }

        [BindProperty]
        public StateCountFilter StateCount { get; set; }

        public async Task OnGetAsync()
        {
            try
            {
                // Fetch data from the API
                var userSubtopics = await _apiService.GetUserSubtopicsAsync();
                var subtopics = await _apiService.GetSubtopicsAsync();

                // Map UserSubtopics to their corresponding subtopics
                foreach (var userSubtopic in userSubtopics)
                {
                    var subtopic = subtopics.FirstOrDefault(s => s.Id == userSubtopic.SubtopicId);
                    if (subtopic != null)
                    {
                        userSubtopic.Subtopic = subtopic;
                    }
                }

                // Map Topics from Subtopics
                Topics = subtopics.GroupBy(s => s.TopicId)
                    .Select(g => new Topic
                    {
                        Id = g.Key,
                        Name = g.First().Topic?.Name,
                        Description = g.First().Topic?.Description,
                        Subtopics = g.ToList()
                    })
                    .ToList();

                // Initialize StateCount
                StateCount = new StateCountFilter
                {
                    UnassignedTrainees = userSubtopics.Where(us => us.State == TaskState.Open).ToList(),
                    InProgressTrainees = userSubtopics.Where(us => us.State == TaskState.InProgress).ToList(),
                    ReviewTrainees = userSubtopics.Where(us=> us.State == TaskState.Review).ToList(),
                    CompletedTrainees = userSubtopics.Where(us => us.State == TaskState.Completed).ToList()
                };

                StateCount.OpenCount = StateCount.UnassignedTrainees.Count;
                StateCount.InProgressCount = StateCount.InProgressTrainees.Count;
                StateCount.CompletedCount = StateCount.CompletedTrainees.Count;
                StateCount.ReviewCount = StateCount.ReviewTrainees.Count;

                UserSubtopics = userSubtopics;
            }
            catch (Exception ex)
            {
                // Handle errors gracefully
                UserSubtopics = new List<UserSubtopic>();
                Topics = new List<Topic>();
                StateCount = new StateCountFilter
                {
                    UnassignedTrainees = new List<UserSubtopic>(),
                    InProgressTrainees = new List<UserSubtopic>(),
                    CompletedTrainees = new List<UserSubtopic>()
                };
                ModelState.AddModelError("", "Failed to load data. Please try again.");
                Console.WriteLine($"Error in OnGetAsync: {ex}");
            }
        }

        public async Task<IActionResult> OnPostChangeStateAsync(int userId, int subtopicId, TaskState state)
        {
            var result = await _apiService.UpdateUserSubtopicStateAsync(userId, subtopicId, state);
            if (result)
            {
                return RedirectToPage(); // Refresh the page after updating the state
            }
            ModelState.AddModelError("", "Failed to update the state. Please try again.");
            return Page();
        }
    }
}
