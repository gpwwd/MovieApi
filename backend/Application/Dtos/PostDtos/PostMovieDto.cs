using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.PostDtos;

public record PostMovieDto
{
    [Required(ErrorMessage = "Title is required")]
    [MaxLength(50, ErrorMessage = "Maximum length for the Name is 50 characters.")]
    public required string Name { get; init; }

    [MaxLength(50, ErrorMessage = "Maximum length for the Name is 50 characters.")]
    public string? AlternativeName { get; init; } = string.Empty;

    public RatingPostDto? Rating { get; init; } = null;
    
    [Required(ErrorMessage = "Type is required")]
    [MaxLength(30, ErrorMessage = "Maximum length for the Type is 30 characters.")]
    public required string Type { get; init; }
    
    [Required(ErrorMessage = "Year is required")]
    [Range(1900, 2100, ErrorMessage = "Year must be between 1900 and 2100.")]
    public required int Year { get; init; }
    public BudgetPostDto? Budget { get; init; } = null;
    
    [Required(ErrorMessage = "Movie Length is required")]
    [Range(15, 400, ErrorMessage = "Movie length im minutes must be between 15 and 400.")]
    public int MovieLength { get; init; }
    public List<string> GenresNames { get; init; } = null!;
    public List<string> CountriesNames { get; init; } = null!;
    
    [Range(1, 250, ErrorMessage = "Price must be between 1 and 250.")]
    public int? Top250 { get; init; }
    
    [Required(ErrorMessage = "Is series field is required")]
    public bool IsSeries { get; init; }
    public ImagePostDto? ImageInfo { get; init; }
    
    public string? Description { get; init; }
    
    [MaxLength(150, ErrorMessage = "Maximum length for short description is 150 characters.")]
    public string? ShortDescription { get; init; }
}