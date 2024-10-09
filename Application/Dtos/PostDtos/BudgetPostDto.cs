namespace Application.Dtos.PostDtos;

public record BudgetPostDto
{
    public string? Currency { get; init; } 
    public double Value { get; init; } 
};