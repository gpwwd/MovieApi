using System.ComponentModel.DataAnnotations;
using Application.Validators;

namespace Application.Dtos.PostDtos;

[RatingValidate]
public record RatingPostDto
{
    [Range(0, 10, ErrorMessage = "Rating must be between 0 and 10")]
    public short? Kp { get; init; }
    
    [Range(0, 10, ErrorMessage = "Rating must be between 0 and 10")]
    public short? Imdb { get; init; }
    
    [Range(0, 10, ErrorMessage = "Rating must be between 0 and 10")]
    public short? FilmCritics { get; init; }
    
}