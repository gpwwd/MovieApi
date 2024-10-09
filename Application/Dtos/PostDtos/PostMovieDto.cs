namespace Application.Dtos.PostDtos;

public record PostMovieDto
{
    public required string Name { get; init; }
    public string? AlternativeName { get; init; } = string.Empty;
    public RatingPostDto? Rating { get; init; }
    public required string Type { get; init; }
    public required int Year { get; init; }
    public BudgetPostDto Budget { get; init; } = new BudgetPostDto();
    public int MovieLength { get; init; }
    public List<string> GenresNames { get; init; } = null!;
    public List<string> CountriesNames { get; init; } = null!;
    public int Top250 { get; init; }
    public bool IsSeries { get; init; }
    public ImagePostDto? ImageInfo { get; init; }
    public string? Description { get; init; }
    public string? ShortDescription { get; init; }
}