namespace MovieApiMvc.Models.Dtos
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
        public double? BudgetValue { get; set; }
        public string? BudgetCurrency { get; set; } = string.Empty;
        public int MovieLength { get; set; }
        public List<string>? Genres { get; set; } = new List<string>();
        public List<string>? Countries { get; set; } = new List<string>();
        public int Top250 { get; set; }
        public bool IsSeries { get; set; }
        public ImageInfoDto? ImageInfo { get; set; }
        public Guid Id { get; set; }
        public string? Description { get; set; }
        public string? ShortDescription { get; set; }
    }
}