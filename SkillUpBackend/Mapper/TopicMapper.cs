using SkillUpBackend.Model;
using SkillUpBackend.ViewModel;

namespace SkillUpBackend.Mapper
{
    public class TopicMapper
    {
        public static TopicViewModel ConvertToTopicViewModel(Topic topic)
        {
            TopicViewModel topicViewModel = new TopicViewModel
            {
                Id = topic.Id,
                TopicName = topic.Name,
                SubtopicCount = topic.Subtopics.Count
            };
            return topicViewModel;
        }
    }
}
