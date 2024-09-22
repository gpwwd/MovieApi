using MovieApiMvc.DataBaseAccess.Entities.MovieEntities;
using MovieApiMvc.DataBaseAccess.Repositories.Contracts;

namespace MovieApiMvc.DataBaseAccess.Repositories;

public class MovieRepository : RepositoryBase<MovieEntity>, IMovieRepository
{
    public MovieRepository(MovieDataBaseContext context)
        : base(context)
    {
    }
}