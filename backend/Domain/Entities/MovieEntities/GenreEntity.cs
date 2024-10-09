using System.Text.Json.Serialization;

namespace Domain.Entities.MovieEntities
{
    public class GenreEntity
    {
        [JsonIgnore]
        public Guid Id { get; set; } = default;
        public string Name { get; set; } = String.Empty;
        public List<MovieEntity>? Movies { get; set; }
    }

}