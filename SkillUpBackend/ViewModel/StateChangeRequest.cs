namespace SkillUpBackend.ViewModel
{
    public class StateChangeRequest
    {
        public required string NewState { get; set; } // The target state
        public required string UpdatedBy { get; set; } // User who updated the state
        public required string Role { get; set; } // "Trainee" or "Mentor"
    }
}
