namespace SkillUpBackend.ViewModel
{
    public class CreationSubTaskViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int SubTopicId { get; set; }
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public string? CreatedBy { get; set; }
    }
}
