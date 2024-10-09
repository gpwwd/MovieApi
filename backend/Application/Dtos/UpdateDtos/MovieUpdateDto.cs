using Application.Dtos.PostDtos;

namespace Application.Dtos.UpdateDtos;

public record UpdateMovieDto
{
    //add validation attributes 
    //and use model.IsValid() method in controller
    public required string Name { get; init; }
    public string? AlternativeName { get; init; } = string.Empty;
    public RatingPostDto? Rating { get; init; }
    public required string Type { get; init; }
    public required int Year { get; init; }
    public BudgetPostDto Budget { get; init; } = new BudgetPostDto();
    public int MovieLength { get; init; }
    public List<string>? GenresNames { get; init; } = null!;
    public List<string>? CountriesNames { get; init; } = null!;
    public int Top250 { get; init; }
    public bool IsSeries { get; init; }
    public ImagePostDto? ImageInfo { get; init; } = new ImagePostDto();// создаем новую картинку
    public string? Description { get; init; }
    public string? ShortDescription { get; init; }
}