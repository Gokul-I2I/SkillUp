namespace SkillUpBackend.Model
{
    public class ApiResponse<T>
    {
        public T Result { get; set; }
        public List<T> Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
