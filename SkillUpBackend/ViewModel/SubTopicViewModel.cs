using SkillUpBackend.Model;

namespace SkillUpBackend.ViewModel
{
    public class SubTopicViewModel
    {
        public int Id { get; set; }
        public int TopicId { get; set; }
        public string TopicName { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Description { get; set; }
        public TaskState TaskState { get; set; }
        public string TimeTaken { get; set; }
    }
}
