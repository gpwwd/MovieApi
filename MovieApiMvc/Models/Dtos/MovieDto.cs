namespace MovieApiMvc.Dtos
{
    public class MovieDto
    {
        public required string Name { get; set; }
        public double? Rate { get; set; }
        public required string Type { get; set; }
        public required int Year { get; set; }
        public Guid Id { get; set; }
        public MovieDto(string name, double rate, Guid id)
        {
            Name = name;
            Rate = rate;
            Id = id;
        }

        public MovieDto()
        {
        }
    }
}