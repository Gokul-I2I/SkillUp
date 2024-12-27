using System.ComponentModel.DataAnnotations.Schema;

namespace SkillUpBackend.Model
{
    public class BatchUser
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int BatchId { get; set; }
        public Batch Batch { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime JoinDate { get; set; } = DateTime.Now;

        [Column(TypeName = "VARCHAR(25)")]
        public required string InsertedBy { get; set; }
        [Column(TypeName = "VARCHAR(25)")]
        public string? UpdatedBy { get; set; }
        public DateTime InsertedOn { get; set; } = DateTime.Now;
        public DateTime? UpdatedOn { get; set; }
    }
}
