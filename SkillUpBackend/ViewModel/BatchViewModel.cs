using SkillUpBackend.Model;

namespace SkillUpBackend.ViewModel
{
    public class BatchViewModel
    {
        public int BatchId { get; set; }
        public string BatchName { get; set; }
        public ICollection<UserCreateOrEdit> UserCreateOrEdits { get; set; }
    }
}