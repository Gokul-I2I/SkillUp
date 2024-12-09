using System.ComponentModel.DataAnnotations.Schema;

namespace SkillUpBackend.Model
{
    public class Role
    {
        public int Id { get; set; }
        [Column(TypeName = "VARCHAR(20)")]
        public required string Name { get; set; }
        public bool IsActive { get; set; } = true;
        [Column(TypeName = "VARCHAR(25)")]
        public required string InsertedBy { get; set; }
        [Column(TypeName = "VARCHAR(25)")]
        public string? UpdatedBy { get; set; }
        public required DateTime InsertedOn { get; set; }
        public DateTime? LastUpdatedOn { get; set; }
        //public ICollection<User> Users { get; set; }
    }
}
