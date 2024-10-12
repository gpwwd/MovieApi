using System.ComponentModel.DataAnnotations;
using Application.IServices;

namespace Application.Validators;

public class UsernameUniqueAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var usernameProperty = validationContext.ObjectType.GetProperty("UserName");
        var usernameValue = usernameProperty?.GetValue(validationContext.ObjectInstance)!.ToString();
        IUsersService? service = validationContext.GetService(typeof(IUsersService)) as IUsersService;
        if (service == null)
            throw new NullReferenceException();
        if (usernameValue == null)
            return new ValidationResult("Username cannot be null.");
        
        var entity = service.GetByName(usernameValue);

        if (entity != null)
            return new ValidationResult(GetErrorMessage(usernameValue));
        
        return ValidationResult.Success;
    }

    private string GetErrorMessage(string email)
    {
        return $"Email {email} is already in use.";
    }
}