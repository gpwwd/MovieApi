namespace MovieApiMvc.ErrorHandling;

public class MyExeption : Exception
{

    public int StatusCode { get; }
    public string? StackTraceDetail { get; } = String.Empty;

    public MyExeption(int statusCode, string message, string? stackTraceDetail = null)
        : base(message)
    {
        StatusCode = statusCode;
        StackTraceDetail = stackTraceDetail;
    }

    public override string ToString()
    {
        return $"StatusCode: {StatusCode}, Message: {Message}, StackTrace: {StackTraceDetail}";
    }

}
