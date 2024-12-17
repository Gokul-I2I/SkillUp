using System.ComponentModel.DataAnnotations;

namespace SkillUpBackend.Model
{
    public class LoginRequest
    {
        public required string Email { get; set; }
        [StringLength(maximumLength: 8, MinimumLength = 6, ErrorMessage = "Password must be 6 or 8 characters long.")]

        public required string Password { get; set; }
    }
}
