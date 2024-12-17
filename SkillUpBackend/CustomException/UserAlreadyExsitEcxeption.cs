namespace SkillUpBackend.CustomException
{
    [Serializable]
    public class UserAlreadyExitException : Exception
    {
        public UserAlreadyExitException()
        {

        }

        public UserAlreadyExitException(string? message) : base(message)
        {
        }
    }
}
