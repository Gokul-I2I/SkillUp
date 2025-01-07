using SkillUpBackend.Model;
using SkillUpBackend.ViewModel;

namespace SkillUpBackend.Mapper
{
    public class SubTopicMapper
    {
        public static SubTopicViewModel ConvertToSubTopicViewModel(UserSubtopic userSubTopic)
        {
            SubTopicViewModel subTopicViewModel = new SubTopicViewModel
            {
                Id = userSubTopic.Subtopic.Id,
                TopicId = userSubTopic.Subtopic.TopicId,
                TopicName = userSubTopic.Subtopic.Topic.Name,
                Name = userSubTopic.Subtopic.Name,
                Description = userSubTopic.Subtopic.Description,
                StartDate = userSubTopic.AssignedOn,
                EndDate = userSubTopic.DueDate,             
                TaskState = userSubTopic.State,
                TimeTaken = userSubTopic.TimeTaken
            };
            return subTopicViewModel;
        }
    }
}
