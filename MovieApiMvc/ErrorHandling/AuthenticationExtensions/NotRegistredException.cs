namespace MovieApiMvc.ErrorHandling.AuthenticationExtensions;

public class NotRegistredException : MyAuthenticationException
{
    public NotRegistredException(string email)
        :base ($"The user with email: {email} is not registred.")
    {
    }
}