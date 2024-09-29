namespace MovieApiMvc.ErrorHandling.AuthenticationExtensions;

public class IncorrectPasswordException : AuthenticationException
{
    public IncorrectPasswordException (string password)
        :base ($"The password: {password} is not correct.")
    {
    }
}