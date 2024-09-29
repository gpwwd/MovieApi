namespace MovieApiMvc.ErrorHandling.AuthenticationExtensions;

public abstract class AuthenticationException : Exception
{
    
    protected AuthenticationException(string message) : base(message)
    {
        
    }
}