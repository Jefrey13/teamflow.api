namespace teamflow.API.Helpers
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = default!;
        public T? Data { get; set; }
        public IEnumerable<string>? Errors { get; set; }
    }
}
