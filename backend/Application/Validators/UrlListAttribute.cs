using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Application.Validators;

public class UrlListAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var urlList = value as List<string>;
        
        if(urlList == null)
            return new ValidationResult("url list must be of type List<string>");
        
        if (urlList.Any(url => !IsValidUrl(url)))
            return new ValidationResult("url list must be a valid URL");

        return ValidationResult.Success;
    }

    private bool IsValidUrl(string url)
    {
        string pattern = @"^(http|https)://.+";
        return Regex.IsMatch(url, pattern);
    }
}