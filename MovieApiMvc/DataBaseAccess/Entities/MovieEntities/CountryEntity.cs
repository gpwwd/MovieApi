using System.Text.Json.Serialization;

namespace MovieApiMvc.DataBaseAccess.Entities.MovieEntities
{
    public class CountryEntity
    {
        [JsonIgnore]
        public Guid Id { get; set; } = default;
        public string Name { get; set; } = String.Empty;
        public List<MovieEntity>? Movies { get; set; }
    }

}