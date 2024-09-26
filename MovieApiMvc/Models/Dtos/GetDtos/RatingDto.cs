namespace MovieApiMvc.Models.Dtos.GetDtos;

public record RatingDto
{
    public Guid Id { get; init; } 
    public short? Kp { get; init; }
    public short? Imdb { get; init; }
    public short? FilmCritics { get; init; }
    public Guid? MovieId { get; init; } 
}