namespace SkillUpBackend.Model
{
    public class Subtopic
    {
        public int Id { get; set; }

        // Foreign key
        public int TopicId { get; set; }

        // Navigation property for Topic
        public Topic? Topic { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
