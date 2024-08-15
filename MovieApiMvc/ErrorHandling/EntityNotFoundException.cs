namespace MovieApiMvc.ErrorHandling
{
    public class EntityNotFoundException : MyExeption
    {
        public int StatusCode { get; }
        public string? StackTraceDetail { get; } = String.Empty;

        public EntityNotFoundException(int statusCode, string message, string? stackTraceDetail = null)
            : base(statusCode, message)
        {
            StackTraceDetail = stackTraceDetail;
        }

        public override string ToString()
        {
            return $"StatusCode: {StatusCode}, Message: {Message}, StackTrace: {StackTraceDetail}";
        }
    }
}