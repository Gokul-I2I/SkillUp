﻿namespace SkillUpBackend.CustomException

{
    [Serializable]
    internal class UserNotFoundException : Exception
    {

        public UserNotFoundException()
        {
        }
        public UserNotFoundException(string? message) : base(message)
        {
        }
    }
}
