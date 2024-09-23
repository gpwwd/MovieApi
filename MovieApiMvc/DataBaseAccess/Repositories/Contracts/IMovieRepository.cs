using MovieApiMvc.DataBaseAccess.Entities.MovieEntities;
using MovieApiMvc.Models.Dtos.GetDtos;
using MovieApiMvc.RequestFeatures;

namespace MovieApiMvc.DataBaseAccess.Repositories.Contracts;

public interface IMovieRepository
{
    public Task<List<MovieEntity>> GetAll(bool trackChanges);
    public Task<List<MovieEntity>> GetWithPaging(MovieParameters movieParams, bool trackChanges);
    public Task<List<MovieEntity>> GetAllWithImages(bool trackChanges);
    public Task<MovieEntity?> GetById(Guid id, bool trackChanges);
    public Task CreateMovie(MovieEntity movieEntity, List<string> genresNames, 
        List<string> countriesNames, Guid? ratingId);
    public void DeleteMovie(MovieEntity movieEntity);
    public Task UpdateMovie(MovieEntity movieEntity, IEnumerable<string>? genresNames,
        IEnumerable<string>? countriesNames, Guid? ratingId);
    // public Task PutPoster(Guid id, ImageInfoDto image);

}