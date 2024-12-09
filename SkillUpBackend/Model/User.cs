using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SkillUpBackend.Model
{
    public class User
    {
        public int Id { get; set; }

        [Column(TypeName = "VARCHAR(25)")]
        public required string FirstName { get; set; }

        [Column(TypeName = "VARCHAR(25)")]
        public required string LastName { get; set; }

        public DateTime? DateOfBirth { get; set; }
        [EmailAddress]
        [Column(TypeName = "VARCHAR(50)")]
        public required string Email { get; set; }
        [Column(TypeName = "VARCHAR(20)")]
        public string? Qualification { get; set; }
        [Column(TypeName = "VARCHAR(70)")]
        public required string Password { get; set; }
        public bool IsActive { get; set; } = true;

        [Column(TypeName = "VARCHAR(25)")]
        public required string InsertedBy { get; set; }

        [Column(TypeName = "VARCHAR(25)")]
        public string? UpdatedBy { get; set; }
        public DateTime InsertedOn { get; set; } = DateTime.Now;
        public DateTime? LastUpdatedOn { get; set; }
        public int RoleId { get; set; } 
        public required Role Role { get; set; }
    }
}
