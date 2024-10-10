using System.ComponentModel.DataAnnotations;
using Application.Validators;

namespace Application.Dtos.PostDtos;

public record BudgetPostDto
{
    [Currency]
    public string Currency { get; init; } = String.Empty;
    
    [Required(ErrorMessage = "Value is required.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Value must be greater than zero.")]
    [DataType(DataType.Currency)]
    public double Value { get; init; } 
};