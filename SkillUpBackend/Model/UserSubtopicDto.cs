namespace SkillUpBackend.Model
{
    public class UserSubtopicDto
    {
        public string UserSubtopicId { get; set; } // Composite key representation
        public int SubtopicId { get; set; }
        public string SubtopicName { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public TaskState State { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime AssignedOn { get; set; }
    }

}
