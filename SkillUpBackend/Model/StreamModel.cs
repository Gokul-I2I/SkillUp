using System.ComponentModel.DataAnnotations.Schema;

namespace SkillUpBackend.Model
{
    public class StreamModel
    {
        public int Id { get; set; }
        [Column(TypeName = "VARCHAR(25)")]

        public required string Name { get; set; }
        [Column(TypeName = "VARCHAR(50)")]
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;

        [Column(TypeName = "VARCHAR(25)")]
        public required string InsertedBy { get; set; }
        [Column(TypeName = "VARCHAR(25)")]
        public string? UpdatedBy { get; set; }
        public DateTime InsertedOn { get; set; } = DateTime.Now;
        public DateTime? UpdatedOn { get; set; }
        public ICollection<BatchStream>? BatchStreams { get; set; }
        public ICollection<Topic>? Topics { get; set; } = new List<Topic>();
    }
}
