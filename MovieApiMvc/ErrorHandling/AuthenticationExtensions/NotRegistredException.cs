namespace MovieApiMvc.ErrorHandling.AuthenticationExtensions;

public class NotRegistredException : AuthenticationException
{
    public NotRegistredException(string email)
        :base ($"The user with email: {email} is not registred.")
    {
    }
}