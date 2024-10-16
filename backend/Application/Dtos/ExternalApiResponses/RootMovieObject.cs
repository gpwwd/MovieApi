using Domain.Entities.MovieEntities;

namespace Application.Dtos.ExternalApiResponses;

public record RootMovieObject
{
    public List<MovieEntity> Docs { get; set; }
    public int Total { get; set; }
    public int Limit { get; set; }
    public int Page { get; set; }
    public int Pages { get; set; }
}
