using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MovieApiMvc.DataBaseAccess.Entities
{
    public class RatingEntity
    {
        [JsonIgnore]
        public Guid Id { get; set; } = default;
        public double? kp { get; set; } = default;
        public double? imdb { get; set; } = default;
        public double? filmCritics { get; set; } = default;
        public double? russianFilmCritics { get; set; } = default;
        public Guid MovieId { get; set; }
        [ForeignKey("MovieId")]
        public MovieEntity? Movie { get; set; } = default;
    }
}