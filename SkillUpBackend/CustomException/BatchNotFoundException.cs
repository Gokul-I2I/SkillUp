namespace SkillUpBackend.CustomException
{
    [Serializable]
    public class BatchNotFoundException : Exception
    {
        public BatchNotFoundException()
        {
        }
        public BatchNotFoundException(string? message) : base(message)
        {
        }
    }
}
