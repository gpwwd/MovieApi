namespace MovieApiMvc.ErrorHandling.AuthenticationExtensions;

public abstract class MyAuthenticationException : Exception
{
    
    protected MyAuthenticationException(string message) : base(message)
    {
        
    }
}