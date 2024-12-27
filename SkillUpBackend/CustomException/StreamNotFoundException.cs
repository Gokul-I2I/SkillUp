namespace SkillUpBackend.CustomException
{
    public class StreamNotFoundException : Exception
    {
        public StreamNotFoundException()
        {
        }
        public StreamNotFoundException(string? message) : base(message)
        {
        }
    }
}
