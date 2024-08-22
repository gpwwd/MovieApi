using Microsoft.AspNetCore.Routing.Patterns;

namespace MovieApiMvc.Dtos
{
    public class MovieDto
    {
        public required string Name { get; set; }
        public string? AlternativeName { get; set; } = string.Empty;
        public double? RatingKp { get; set; }
        public double? RatingImdb { get; set; }
        public double? RatingFilmCritics { get; set; }
        public required string Type { get; set; }
        public required int Year { get; set; }
        public double? BudgetValue { get; set; } = default;
        public string? BudgetCurrency { get; set; } = string.Empty;
        public int MovieLength { get; set; } = default;
        public List<string>? Genres { get; set; } = new List<string>();
        public List<string>? Countries { get; set; }= new List<string>();
        public int Top250 { get; set; } = default;
        public bool IsSeries { get; set; } = default;
        public ImageInfoDto? ImageInfo { get; set; } = default;
        public Guid Id { get; set; }
        public string? Description { get; set; }
        public string? ShortDescription { get; set; }
        public MovieDto()
        {
        }
    }
}