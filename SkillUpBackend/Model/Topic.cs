namespace SkillUpBackend.Model
{
    public class Topic
    {
        public int Id { get; set; }

        public required string Name { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }

        // Navigation property for related Subtopics
        public ICollection<Subtopic>? Subtopics { get; set; }
    }
}
