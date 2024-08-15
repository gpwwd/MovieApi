using MovieApiMvc.DataBaseAccess.Entities.UsersEntities;
using Newtonsoft.Json;

namespace MovieApiMvc.DataBaseAccess.Entities
{
    public class MovieEntity
    {
        [JsonIgnore]
        public Guid Id { get; set; } = default;
        public string Name { get; set; } = string.Empty;
        public string? AlternativeName { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public int Year { get; set; }= default;
        //один к одному
        public RatingEntity Rating { get; set; }
        //один к одному
        public BudgetEntity? Budget { get; set; } = default;
        public int MovieLength { get; set; } = default;
        public List<GenreEntity>? Genres { get; set; } = new List<GenreEntity>();
        public List<CountryEntity>? Countries { get; set; }= new List<CountryEntity>();
        public int Top250 { get; set; } = default;
        public bool IsSeries { get; set; } = default;
        //это надо настроить в конфиге юзера
        public List<UserEntity>? FavMovieUsers { get; set; } = default;
        public List<UserEntity>? WatchLaterUsers { get; set; } = default;
        public MovieEntity() { }
    }
}