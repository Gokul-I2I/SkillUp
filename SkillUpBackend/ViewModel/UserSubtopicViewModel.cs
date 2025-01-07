using SkillUpBackend.Model;

namespace SkillUpBackend.ViewModel
{
    public class UserSubtopicViewModel
    {
        public int UserId { get; set; } 
        public int SubtopicId { get; set; } 
        public int TopicId { get; set; }
        public string Username { get; set; }
        public TaskState State { get; set; }
        public DateTime? DueDate { get; set; }           
        public DateTime AssignedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public string TimeTaken { get; set; } 
    }
}
