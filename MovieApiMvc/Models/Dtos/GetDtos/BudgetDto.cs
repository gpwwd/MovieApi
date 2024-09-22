namespace MovieApiMvc.Models.Dtos.GetDtos;

public record BudgetDto
{
    public Guid Id { get; init; } 
    public string? Currency { get; init; } 
    public double Value { get; init; } 
    public Guid MovieId { get; init; } 
}