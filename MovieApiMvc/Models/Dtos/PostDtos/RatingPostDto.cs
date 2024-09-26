namespace MovieApiMvc.Models.Dtos.PostDtos;

public record RatingPostDto
{
    public short? Kp { get; init; }
    public short? Imdb { get; init; }
    public short? FilmCritics { get; init; }
}