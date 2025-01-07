using Microsoft.AspNetCore.JsonPatch;
using SkillUpBackend.Model;
using SkillUpBackend.ViewModel;

namespace SkillUpBackend.Service
{
    public interface ITraineeService
    {
        Task<IEnumerable<TopicViewModel>> GetAllTopicAsync();
        Task<IEnumerable<SubTopicViewModel>> GetAllSubTopicByTopicIdandEmailAsync(int topicId, string email);
        Task<SubTaskViewModel?> GetSubTaskViewModelByIdAsync(int id);
        Task<IEnumerable<SubTaskViewModel>> GetAllSubTaskViewModelAsync();
        Task UpdateTimeTaken(int subtopicId, string email);
        Task<bool> CreateSubTaskAsync(CreationSubTaskViewModel viewModel);
        Task<bool> UpdateSubTaskAsync(int id, JsonPatchDocument<SubTask> viewModel);
        Task<bool> DeleteSubTaskAsync(int id);
        Task<IEnumerable<SubTaskViewModel>> GetAllSubTasksBySubTopicIdAsync(int id);
        Task<IEnumerable<UserSubtopicViewModel>> GetUserSubtopicByEmailAsync(string email);
    }
}
