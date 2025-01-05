using System.ComponentModel.DataAnnotations;

namespace SkillUpBackend.ViewModel
{
    public class BatchCreateOrEdit
    {
        public int? Id { get; set; }
        [StringLength(maximumLength: 25)]
        public required string Name { get; set; }
        public ICollection<int>? UserId { get; set; }
        public DateTime JoinDate { get; set; } = DateTime.Now;
        public DateTime InsertedOn { get; set; } = DateTime.Now;
        public DateTime? UpdatedOn { get; set; }
        public string InsertedBy { get; set; }
        public bool IsActive {  get; set; }
        public string? UpdatedBy { get; set; }
    }
}
