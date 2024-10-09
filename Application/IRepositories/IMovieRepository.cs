using Application.RequestFeatures;
using Domain.Entities.MovieEntities;

namespace Application.IRepositories;

public interface IMovieRepository
{
    public Task<List<MovieEntity>> GetAll(bool trackChanges);
    public Task<List<MovieEntity>> GetWithPaging(MovieParameters movieParams, bool trackChanges);
    public Task<List<MovieEntity>> GetAllWithImages(bool trackChanges);
    public Task<MovieEntity?> GetById(Guid id, bool trackChanges);
    public Task CreateMovie(MovieEntity movieEntity, List<string> genresNames, 
        List<string> countriesNames);
    public void DeleteMovie(MovieEntity movieEntity);
    public Task UpdateMovie(MovieEntity movieEntity, IEnumerable<string>? genresNames,
        IEnumerable<string>? countriesNames, short? Imdb, short? Kp, short? FilmCritics);
    // public Task PutPoster(Guid id, ImageInfoDto image);

}