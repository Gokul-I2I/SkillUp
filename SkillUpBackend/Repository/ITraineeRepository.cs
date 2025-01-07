using SkillUpBackend.Model;

namespace SkillUpBackend.Repository
{
    public interface ITraineeRepository
    {
        Task<IEnumerable<Topic>> GetAllTopicAsync();
        Task<IEnumerable<UserSubtopic>> GetAllUserSubTopicByTopicIdandEmailAsync(int topicId, string email);
        Task<SubTask?> GetSubTaskByIdAsync(int id);
        Task<IEnumerable<SubTask>> GetAllSubTaskAsync();
        Task<bool> AddSubTaskAsync(SubTask subTask);
        Task<bool> UpdateSubTaskAsync(SubTask subTask);
        Task<bool> DeleteSubTaskAsync(int id);
        Task<IEnumerable<SubTask>> GetAllSubTaskBySubTopicIdAsync(int subTopicId);
        Task<IEnumerable<UserSubtopic>> GetUserSubtopicByEmailAsync(string email);
        Task<UserSubtopic> GetUserSubtopicByEmailandSubtopicIdAsync(string email, int subtopicId);
        Task UpdateUserSubtopicAsync(UserSubtopic userSubtopic);
    }
}
