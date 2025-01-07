using SkillUpBackend.Model;
using SkillUpBackend.ViewModel;

namespace SkillUpBackend.Mapper
{
    public class SubTaskMapper
    {
        public static SubTaskViewModel ConvertToSubTaskViewModel(SubTask subtask)
        {
            SubTaskViewModel subTaskViewModel = new SubTaskViewModel
            {
                Id = subtask.Id,
                SubTopicId = subtask.SubTopicId,
                SubTopicName = subtask.SubTopic.Name,
                Title = subtask.Title,
                Hours = subtask.Hours,
                Minutes = subtask.Minutes
            };
            return subTaskViewModel;
        }
    }
}
