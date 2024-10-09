using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entities.MovieEntities;

public class BudgetEntity
{
    [JsonIgnore]
    public Guid Id { get; set; }
    public string? Currency { get; set; }
    public double Value { get; set; }
    public Guid MovieId { get; set; }
    [ForeignKey("MovieId")]
    public MovieEntity? Movie { get; set; } = default;
}