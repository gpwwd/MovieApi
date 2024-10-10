using System.ComponentModel.DataAnnotations;
using Application.Dtos.PostDtos;
using Domain.Enums;

namespace Application.Validators;

public class CurrencyAttribute : ValidationAttribute
{
    public string Error { get; set; } = String.Empty;
    
    protected override ValidationResult? IsValid(object? value, ValidationContext
        validationContext)
    {
        Error = String.Empty;
        var budgetDto = (BudgetPostDto)validationContext.ObjectInstance;
        bool checkCurrencyEnum = true;
        
        if (string.IsNullOrEmpty(budgetDto.Currency) || budgetDto.Currency.Length < 3)
        {
            checkCurrencyEnum = false;
            Error = $"{nameof(budgetDto.Currency)} cannot be null or empty or shorter than three characters.";
        }

        if(!budgetDto.Currency.All(c => Char.IsUpper(c)))
            Error = $"{nameof(budgetDto.Currency)} is not valid. All letters must be upper case.";
        
        if (checkCurrencyEnum)
        {
            var currencies = Enum.GetValues(typeof(Currency))
                .Cast<Currency>()
                .Select(day => day.ToString()) 
                .ToList();
            var currency = budgetDto.Currency[0] + budgetDto.Currency.Substring(1).ToLower();
            if (!currencies.Contains(currency))  
                Error = $"{nameof(budgetDto.Currency)} is invalid.";
        }

        if(Error != String.Empty)
            return new ValidationResult(Error);
        
        return ValidationResult.Success;    
    }
}

