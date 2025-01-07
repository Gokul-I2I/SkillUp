using Microsoft.AspNetCore.JsonPatch.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using SkillUp.Services;
using SkillUpBackend.Mapper;
using SkillUpBackend.Model;
using SkillUpBackend.ViewModel;
using System.Net.Http;
using System.Runtime.CompilerServices;

namespace SkillUp.Pages.TraineeDashBoard
{
    public class IndexModel : PageModel
    {
        private readonly ITraineeService _traineeService;
        private readonly ApiService _apiService;
        private readonly string _apiBaseUrl;
        private readonly HttpClient _httpClient;

        public List<TopicViewModel> Topics { get; set; }
        public Dictionary<int, List<SubTopicViewModel>> SubTopics { get; set; } = new();
        public Dictionary<int, List<SubTaskViewModel>> SubTasks { get; set; } = new();
        public List<UserSubtopicViewModel> UserSubtopics { get; set; } = new();

        [BindProperty]
        public CreationSubTaskViewModel SubTask { get; set; }

        public IndexModel(HttpClient httpClient, ITraineeService traineeService, ApiService apiService, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _traineeService = traineeService;
            _apiService = apiService;
            _apiBaseUrl = configuration["ApiBaseUrl"];
        }

        public async Task<List<TopicViewModel>> FetchTopicsAsync()
        {
            var topics = await _traineeService.GetAllTopicsAsync();

            if (Request.Cookies.TryGetValue("UserEmail", out string email))
            {
                if (!string.IsNullOrEmpty(email))
                {
                    // Fetch user-specific subtopics
                    UserSubtopics = await _traineeService.GetUserSubtopicByEmailAsync(email);
                    Console.Error.WriteLine(email);

                    if (UserSubtopics.Any())
                    {
                        // Extract TopicIds associated with the user's subtopics
                        var topicIds = UserSubtopics
                            .Select(us => us.TopicId)
                            .Distinct()
                            .ToList();

                        return topics
                            .Where(topic => topicIds.Contains(topic.Id))
                            .ToList();
                    }
                    else
                    {
                        // Handle case when there are no UserSubtopics for the user
                        return new List<TopicViewModel>();
                    }
                }
                else
                {
                    return new List<TopicViewModel>();
                }
            }
            else
            {
                // Handle case where the email cookie is not found
                return new List<TopicViewModel>();
            }
        }

        public async Task<List<SubTopicViewModel>> FetchSubtopicAsync(int topicId) 
        {
            
            if (Request.Cookies.TryGetValue("UserEmail", out string email))
            {
                if (!string.IsNullOrEmpty(email))
                {
                    // Fetch user-specific subtopics
                    var subtopics = await _traineeService.GetSubTopicsByTopicIdandEmailAsync(topicId, email);

                    if (subtopics.Any())
                    {
                        return subtopics;
                    }
                    else
                    {
                        // Handle case when there are no UserSubtopics for the user
                        return new List<SubTopicViewModel>();
                    }
                }
                else
                {
                    return new List<SubTopicViewModel>();
                }
            }
            else
            {
                // Handle case where the email cookie is not found
                return new List<SubTopicViewModel>();
            }
        }

        public async Task OnGet()
        {
            Topics = await FetchTopicsAsync();            
        }
            
        public async Task<IActionResult> OnGetGetSubtopicsAsync(int topicId)
        {
            var subTopics = await FetchSubtopicAsync(topicId);
            if (subTopics != null)
            {
                SubTopics[topicId] = subTopics;
            }
            return new JsonResult(SubTopics[topicId]);
        }

        public async Task<IActionResult> OnGetGetSubtasksAsync(int subTopicId)
        {
            var subTasks = await _traineeService.GetSubTasksBySubTopicIdAsync(subTopicId);
            if (subTasks != null)
            {
                SubTasks[subTopicId] = subTasks;
            }
            return new JsonResult(SubTasks[subTopicId]);
        }

        public async Task<IActionResult> OnPostCreateNewSubTaskAsync()
        {
            if (SubTask == null || string.IsNullOrWhiteSpace(SubTask.Title))
            {
                ModelState.AddModelError(string.Empty, "Invalid input data.");
                Topics = await FetchTopicsAsync(); // Reinitialize Topics

                // Fetch subtasks for the current SubTopic to keep the subtasks view.
                if (SubTask.SubTopicId != 0)
                {
                    SubTasks[SubTask.SubTopicId] = await _traineeService.GetSubTasksBySubTopicIdAsync(SubTask.SubTopicId);
                }
                return Page();
            }

            try
            {
                if (Request.Cookies.TryGetValue("UserEmail", out string email))
                {
                    if (!string.IsNullOrEmpty(email))
                    {
                        SubTask.CreatedBy = email;
                    }
                }
                var isSuccess = await _traineeService.CreateSubTaskAsync(SubTask);
                if (isSuccess)
                {
                    TempData["Message"] = "SubTask created successfully.";
                    // Redirect with subTopicId to stay in the subtasks section.
                    return RedirectToPage(new { subTopicId = SubTask.SubTopicId });
                }
                else
                {
                    TempData["Error"] = "Failed to create SubTask. Please verify the details.";
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error occurred: {ex.Message}";
            }

            Topics = await FetchTopicsAsync(); // Reinitialize Topics if not redirecting
            if (SubTask.SubTopicId != 0)
            {
                SubTasks[SubTask.SubTopicId] = await _traineeService.GetSubTasksBySubTopicIdAsync(SubTask.SubTopicId);
            }
            return Page();
        }

        public async Task<IActionResult> OnPostEditSubTaskAsync()
        {
            if (SubTask == null || string.IsNullOrWhiteSpace(SubTask.Title))
            {
                ModelState.AddModelError(string.Empty, "Invalid input data.");
                Topics = await FetchTopicsAsync(); // Reinitialize Topics

                // Fetch subtasks for the current SubTopic to keep the subtasks view.
                if (SubTask.SubTopicId != 0)
                {
                    SubTasks[SubTask.SubTopicId] = await _traineeService.GetSubTasksBySubTopicIdAsync(SubTask.SubTopicId);
                }
                return Page();
            }

            try
            {
                var isSuccess = await _traineeService.UpdateSubTaskAsync(SubTask);
                if (isSuccess)
                {
                    TempData["Message"] = "SubTask updated successfully.";
                    return RedirectToPage(new { subTopicId = SubTask.SubTopicId });
                }
                else
                {
                    TempData["Error"] = "Failed to update SubTask. Please verify the details.";
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error occurred: {ex.Message}";
            }

            Topics = await FetchTopicsAsync(); // Reinitialize Topics if not redirecting
            if (SubTask.SubTopicId != 0)
            {
                SubTasks[SubTask.SubTopicId] = await _traineeService.GetSubTasksBySubTopicIdAsync(SubTask.SubTopicId);
            }
            return Page();
        }

        public async Task<IActionResult> OnGetDeleteSubTaskAsync(int id)
        {
            try
            {
                var success = await _traineeService.DeleteSubTaskAsync(id);
                if (success)
                {
                    return new JsonResult(new { success = true });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Failed to delete subtask." });
                }
            }
            catch (Exception ex)
            {
                return new JsonResult(new { success = false, message = ex.Message });
            }
        }

        public async Task<IActionResult> OnPostChangeTaskStateAsync(int subtopicId, TaskState state)
        {
            try
            {
                if (Request.Cookies.TryGetValue("UserId", out string userIdCookie))
                {
                    if (int.TryParse(userIdCookie, out int userId))
                    {
                        // Update the task state
                        var result = await _apiService.UpdateUserSubtopicStateAsync(userId, subtopicId, state);
                        if (result)
                        {
                            TempData["Message"] = "Task state updated successfully.";
                        }
                        else
                        {
                            TempData["Error"] = "Failed to update TaskState.";
                        }
                    }
                    else
                    {
                        TempData["Error"] = "Invalid UserId in cookie. Please log in again.";
                    }
                }
                else
                {
                    TempData["Error"] = "UserId not found in cookies. Please log in again.";
                }

                // Reload topics to maintain page state in case of an error
                Topics = await FetchTopicsAsync();
                return Page();

            }
            catch (HttpRequestException ex)
            {
                Console.Error.WriteLine($"Error updating subtopic state: {ex.Message}");
                TempData["Error"] = "An error occurred while updating the subtopic state.";
            }

            Topics = await FetchTopicsAsync(); // Reload topics to maintain page state
            return Page();
        }


    }
}
