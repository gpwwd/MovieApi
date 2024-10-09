namespace Application.Dtos.GetDtos
{
    public class MovieDto
    {
        public required string Name { get; set; }
        public string? AlternativeName { get; set; } = string.Empty;
        public RatingDto Rating { get; set; } = new RatingDto();
        public required string Type { get; set; }
        public required int Year { get; set; }
        public BudgetDto Budget { get; set; } = new BudgetDto();
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