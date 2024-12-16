using SkillUpBackend.Model;

namespace SkillUpBackend.Service
{
    public interface IMentorService
    {
        IEnumerable<Topic> GetTopics();
        Topic? GetTopicById(int id);
        void AddTopic(Topic topic);
        void EditTopic(Topic topic);
        void DeleteTopic(int id);

        IEnumerable<Subtopic> GetSubtopics();
        Subtopic? GetSubtopicById(int id);
        void AddSubtopic(Subtopic subtopic);
        void EditSubtopic(Subtopic subtopic);
        void DeleteSubtopic(int id);
    }
}
