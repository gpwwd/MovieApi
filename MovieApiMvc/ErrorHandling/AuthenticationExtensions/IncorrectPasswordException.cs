namespace MovieApiMvc.ErrorHandling.AuthenticationExtensions;

public class IncorrectPasswordException : MyAuthenticationException
{
    public IncorrectPasswordException (string password)
        :base ($"The password: {password} is not correct.")
    {
    }
}