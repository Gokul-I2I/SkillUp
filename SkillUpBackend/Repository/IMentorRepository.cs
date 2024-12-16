using SkillUpBackend.Model;

namespace SkillUpBackend.Repository
{
    public interface IMentorRepository
    {
        IEnumerable<Topic> GetActiveTopics();
        Topic? GetTopicById(int id);
        void AddTopic(Topic topic);
        void UpdateTopic(Topic topic);
        void DeactivateTopic(int id);

        IEnumerable<Subtopic> GetActiveSubtopics();
        Subtopic? GetSubtopicById(int id);
        void AddSubtopic(Subtopic subtopic);
        void UpdateSubtopic(Subtopic subtopic);
        void DeactivateSubtopic(int id);
    }
}
