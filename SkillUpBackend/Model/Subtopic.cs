using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

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
        public string? UpdatedBy { get; set; }
        public ICollection<UserSubtopic> UserSubtopics { get; set; } = new List<UserSubtopic>();
        public ICollection<SubTask>? SubTasks { get; set; }

    }
}
