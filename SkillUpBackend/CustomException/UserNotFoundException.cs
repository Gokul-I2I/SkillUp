namespace SkillUpBackend.CustomException

{
    [Serializable]
    internal class UserNotFoundException : Exception
    {
        private object value;

        public UserNotFoundException()
        {
        }

        public UserNotFoundException(object value)
        {
            this.value = value;
        }

        public UserNotFoundException(string? message) : base(message)
        {
        }

        public UserNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
