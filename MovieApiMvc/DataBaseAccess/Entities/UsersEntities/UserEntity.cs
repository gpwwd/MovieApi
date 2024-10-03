using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using MovieApiMvc.DataBaseAccess.Entities.MovieEntities;

namespace MovieApiMvc.DataBaseAccess.Entities.UsersEntities
{
    public class UserEntity : IdentityUser<Guid>
    {
        [JsonIgnore]
        public override Guid Id { get; set; } 
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public List<MovieEntity>? FavMovies {get; set;}
        public List<MovieEntity>? WatchLaterMovies {get; set;}
    }
}