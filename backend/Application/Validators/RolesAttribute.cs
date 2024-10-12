using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.JavaScript;
using Domain.Enums;

namespace Application.Validators;

public class RolesAttribute : ValidationAttribute
{
    private string Error  { get; set; } = String.Empty;
    
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        Error  = String.Empty;
        var roles = value as List<string>;

        if(roles == null)
            return new ValidationResult("Roles should not be null");

        foreach (var role in roles)
        { 
            if (!IsValidRole(role))
                if (Error == String.Empty)
                    Error = $"These roles are invalid: {role}";
                else
                    Error += $", {role}";
        }
        
        if(String.IsNullOrEmpty(Error))
            return ValidationResult.Success;
        else 
            return new ValidationResult(Error);
    }
    
    private bool IsValidRole(string role)
    {
        var roles = Enum.GetValues(typeof(Role))
            .Cast<Role>()
            .Select(r =>r.ToString()) 
            .ToList();
        
        if (!roles.Contains(role))
            return false;
        
        return true;
    }
}