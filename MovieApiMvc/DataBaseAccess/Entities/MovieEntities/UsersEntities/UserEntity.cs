using System.Text.Json.Serialization;

namespace MovieApiMvc.DataBaseAccess.Entities.UsersEntities
{
    public class UserEntity
    {
        [JsonIgnore]
        public Guid Id { get; set; } 
        public required string UserName { get; set; }
        public string PasswHash { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public List<MovieEntity>? FavMovies {get; set;} = default;
        public List<MovieEntity>? WatchLaterMovies {get; set;} = new List<MovieEntity>();
    }
}