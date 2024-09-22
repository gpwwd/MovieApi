using MovieApiMvc.DataBaseAccess.Entities.MovieEntities;
using MovieApiMvc.RequestFeatures;

namespace MovieApiMvc.DataBaseAccess.Repositories.Contracts;

public interface IMovieRepository
{
    public Task<List<MovieEntity>> GetAll(bool trackChanges);
    public Task<List<MovieEntity>> GetWithPaging(MovieParameters movieParams, bool trackChanges);
    public Task<List<MovieEntity>> GetAllWithImages(bool trackChanges);
    public Task<MovieEntity?> GetById(Guid id, bool trackChanges);
    void CreateMovie(MovieEntity movieEntity, IEnumerable<Guid> genresId, 
        IEnumerable<Guid> countriesId, Guid ratingId);
}