namespace MovieApiMvc.Models.Dtos.GetDtos;

public record RatingDto
{
    public Guid Id { get; init; } 
    public double? Kp { get; init; }
    public double? Imdb { get; init; }
    public double? FilmCritics { get; init; }
    public Guid? MovieId { get; init; } 
}