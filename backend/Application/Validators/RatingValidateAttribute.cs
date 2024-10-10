using System.ComponentModel.DataAnnotations;
using Application.Dtos.PostDtos;

namespace Application.Validators;

public class RatingValidateAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var ratingDto = value as RatingPostDto;

        if(ratingDto == null)
            return ValidationResult.Success;
        
        if (ratingDto.Kp.HasValue || ratingDto.Imdb.HasValue || ratingDto.FilmCritics.HasValue)
            return ValidationResult.Success; 
        
        return new ValidationResult("At least one rating must be provided.");
    }
}