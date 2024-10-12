using System.ComponentModel.DataAnnotations;
using Application.IRepositories;
using Application.IServices;

namespace Application.Validators;

public class EmailUserUniqueAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        string email = (value as string)!;
        IUsersService? service = validationContext.GetService(typeof(IUsersService)) as IUsersService;
        if (service == null)
            throw new NullReferenceException();
        var entity = service.GetByEmail(email);

        if (entity != null)
            return new ValidationResult(GetErrorMessage(email));

        return ValidationResult.Success;
    }

    private string GetErrorMessage(string email)
    {
        return $"Email {email} is already in use.";
    }
}