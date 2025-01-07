namespace SkillUpBackend.Model
{
    public class SubTask
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int SubTopicId { get; set; }
        public Subtopic SubTopic { get; set; }
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime UpdatedOn { get; set;}
        public string? UpdatedBy { get; set; }
    }
}
