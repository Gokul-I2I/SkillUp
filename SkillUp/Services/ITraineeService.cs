using SkillUpBackend.Model;
using SkillUpBackend.ViewModel;

namespace SkillUp.Services
{
    public interface ITraineeService
    {
        Task<List<SubTopicViewModel>> GetSubTopicsByTopicIdandEmailAsync(int id, string email);
        Task<List<TopicViewModel>> GetAllTopicsAsync();
        Task<List<SubTaskViewModel>> GetSubTasksBySubTopicIdAsync(int id);
        Task<bool> CreateSubTaskAsync(CreationSubTaskViewModel subTask);
        Task<bool> UpdateSubTaskAsync(CreationSubTaskViewModel subTask);
        Task<bool> DeleteSubTaskAsync(int id);
        Task<List<UserSubtopicViewModel>> GetUserSubtopicByEmailAsync(string email);
    }
}
