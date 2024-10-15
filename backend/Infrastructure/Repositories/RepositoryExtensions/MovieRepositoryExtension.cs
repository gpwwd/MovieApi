using Domain.Entities.MovieEntities;

namespace Infrastructure.Repositories.RepositoryExtensions;

public static class MovieRepositoryExtension
{
    public static IQueryable<MovieEntity> FilterMovies(this IQueryable<MovieEntity> query, short minRating, short maxRating, string ratingPlatform)
    {
            switch (ratingPlatform)
            {
                case "Kp":
                    return query.Where(m => m.Rating!.Kp.HasValue && 
                                             m.Rating.Kp >= minRating && 
                                             m.Rating.Kp <= maxRating);
                case "Imdb":
                    return query.Where(m => m.Rating!.Imdb.HasValue && 
                                             m.Rating.Imdb >= minRating && 
                                             m.Rating.Imdb <= maxRating);
                case "FilmCritics":
                    return query.Where(m => m.Rating!.FilmCritics.HasValue && 
                                             m.Rating.FilmCritics >= minRating &&
                                             m.Rating.FilmCritics <= maxRating);
                default:
                    throw new ArgumentException("Invalid rating platform specified.");
            }
    }
}