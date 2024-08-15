using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MovieApiMvc.DataBaseAccess.Entities
{
    public class BudgetEntity
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string? Currency { get; set; }
        public double Value { get; set; }
        //зависимая от Movie сущность 
        //отношение один к одному
        public Guid MovieId { get; set; }
        [ForeignKey("MovieId")]
        public MovieEntity? Movie { get; set; } = default;
    }
}