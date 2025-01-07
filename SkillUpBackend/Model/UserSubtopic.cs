namespace SkillUpBackend.Model
{
    public class UserSubtopic
    {
        public int UserId { get; set; } // Foreign key to User
        public User User { get; set; }
        public int SubtopicId { get; set; } // Foreign key to Subtopic
        public Subtopic Subtopic { get; set; }
        public string Username { get; set; }       

        // Additional metadata for progress tracking
        public TaskState State { get; set; } = TaskState.Open; 
        public DateTime? DueDate { get; set; }             // Due date for the task
        public DateTime AssignedOn { get; set; } = DateTime.Now; // Date of assignment
        public string? UpdatedBy { get; set; }
        public string TimeTaken { get; set; } = "0 hours 0 minutes"; // Default value

    }
}
