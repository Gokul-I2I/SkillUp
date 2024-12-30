using SkillUpBackend.Model;

namespace SkillUpBackend.Service
{
    public interface IUserSubtopicService
    {
        public Task<List<UserSubtopic>> GetUserSubtopicsAsync();
        public Task<bool> UpdateUserSubtopicStateAsync(int userId, int subtopicId, TaskState state);
    }
}
