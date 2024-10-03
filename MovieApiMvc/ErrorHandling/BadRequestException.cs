namespace MovieApiMvc.ErrorHandling;

public abstract class BadRequestException : Exception
{
    protected BadRequestException(string message) : base(message)
    {
        
    }
}