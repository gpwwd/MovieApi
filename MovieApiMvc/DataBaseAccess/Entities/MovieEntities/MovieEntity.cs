using MovieApiMvc.DataBaseAccess.Entities.UsersEntities;
using Newtonsoft.Json;

namespace MovieApiMvc.DataBaseAccess.Entities.MovieEntities
{
    public class MovieEntity
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? AlternativeName { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public int Year { get; set; }
        public RatingEntity? Rating { get; set; } = new RatingEntity();
        //один к одному
        public BudgetEntity? Budget { get; set; }
        public int MovieLength { get; set; }
        public List<GenreEntity> Genres { get; set; } = new List<GenreEntity>();
        public List<CountryEntity> Countries { get; set; }= new List<CountryEntity>();
        public int Top250 { get; set; }
        public bool IsSeries { get; set; }
        public List<UserEntity>? FavMovieUsers { get; set; }
        public List<UserEntity>? WatchLaterUsers { get; set; }
        public ImageInfoEntity? ImageInfoEntity  { get; set; } = new ImageInfoEntity();
        public string? Description { get; set; } = string.Empty;
        public string? ShortDescription { get; set; } = string.Empty;
    }
}