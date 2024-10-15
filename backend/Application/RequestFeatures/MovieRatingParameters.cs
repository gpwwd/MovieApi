using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Application.RequestFeatures;
public class MovieRatingParameters : RequestParameters, IValidatableObject
{
    public short MinRating { get; set; }
    public short MaxRating { get; set; }
    public string RatingPlatform { get; set; } = null!;
    public bool ValidPlatform()
    {
        var platforms = Enum.GetValues(typeof(RatingPlatforms))
        .Cast<RatingPlatforms>()
        .Select(platform => platform.ToString()) 
        .ToList();
        
        if (!platforms.Contains(RatingPlatform) || string.IsNullOrWhiteSpace(RatingPlatform))
            return false;

        return true;
    }
    
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var errorMessage = String.Empty;
        var validationResults = new List<ValidationResult>();

        var validRating = MinRating <= MaxRating;
        var validPlatform = this.ValidPlatform();

        if (!validPlatform)
        {
            errorMessage += $"The rating platform is invalid";
            yield return new ValidationResult(errorMessage, new[] { nameof(RatingPlatform) });
        }

        if (!validRating)
        {
            errorMessage += $"The min and max rating values are invalid";
            yield return new ValidationResult(errorMessage, new[] { nameof(MaxRating), nameof(MinRating) });
        }
    }
}