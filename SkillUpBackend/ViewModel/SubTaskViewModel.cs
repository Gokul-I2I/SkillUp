namespace SkillUpBackend.ViewModel
{
    public class SubTaskViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int SubTopicId { get; set; }
        public string SubTopicName { get; set; }
        public int Hours { get; set; }
        public int Minutes { get; set; }
    }
}
