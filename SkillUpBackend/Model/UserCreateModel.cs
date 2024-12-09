using System.ComponentModel.DataAnnotations;

namespace SkillUpBackend.Model
{
    public class UserCreateModel
    {
        [StringLength(maximumLength:25)]
        public required string FirstName { get; set; }

        [StringLength(maximumLength:25)]
        public required string LastName { get; set; }

        [StringLength(maximumLength:50)]
        [EmailAddress]
        public required string Email { get; set; }

        [StringLength(maximumLength: 8, MinimumLength = 6, ErrorMessage = "Password must be 6 or 8 characters long.")]
        [DataType(DataType.Password)]
        public required string Password { get; set; }

        public required string Role { get; set; }
    }
}
