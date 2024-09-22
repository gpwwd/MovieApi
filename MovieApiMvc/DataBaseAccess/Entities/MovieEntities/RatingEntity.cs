using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MovieApiMvc.DataBaseAccess.Entities.MovieEntities
{
    public class RatingEntity
    {
        [JsonIgnore]
        public Guid Id { get; set; } 
        public double? Kp { get; set; }
        public double? Imdb { get; set; }
        public double? FilmCritics { get; set; } 
        public double? RussianFilmCritics { get; set; } 
        public Guid MovieId { get; set; }
        [ForeignKey("MovieId")]
        public MovieEntity? Movie { get; set; }
    }
}