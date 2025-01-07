using SkillUpBackend.Model;
using SkillUpBackend.ViewModel;

namespace SkillUpBackend.Mapper
{
    public class UserSubtopicMapper
    {
        public static UserSubtopicViewModel ConvertToUserSubtopicViewModel(UserSubtopic userSubtopic)
        {
            UserSubtopicViewModel userSubtopicViewModel = new UserSubtopicViewModel
            {
                UserId = userSubtopic.UserId,
                Username = userSubtopic.Username,
                SubtopicId = userSubtopic.SubtopicId,
                TopicId = userSubtopic.Subtopic.TopicId,
                State = userSubtopic.State,
                TimeTaken = userSubtopic.TimeTaken,
                DueDate = userSubtopic.DueDate,
                AssignedOn = userSubtopic.AssignedOn,
                UpdatedBy = userSubtopic.UpdatedBy
            };
            return userSubtopicViewModel;
        }
    }
}
