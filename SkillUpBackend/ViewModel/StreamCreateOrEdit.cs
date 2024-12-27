using SkillUpBackend.Model;

namespace SkillUpBackend.ViewModel
{
    public class StreamCreateOrEdit
    {
        public int? Id { get; set; }
        public required string Name { get; set; }    
        public string? Description { get; set; }
        public ICollection<Topic>? Topic { get; set; }   
    }
}
