namespace SkillUpBackend.Model
{
    public class Batch
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public bool IsActive { get; set; } = true;

        public required string InsertedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime InsertedOn { get; set; } = DateTime.Now;
        public DateTime? UpdatedOn { get; set; }
        public ICollection<BatchStream>? BatchStreams { get; set; }
        public ICollection<BatchUser>? BatchUsers { get; set; }
    }
}
