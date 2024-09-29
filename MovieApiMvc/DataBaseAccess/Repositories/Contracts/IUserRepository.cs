using MovieApiMvc.DataBaseAccess.Entities.MovieEntities;
using MovieApiMvc.DataBaseAccess.Entities.UsersEntities;

namespace MovieApiMvc.DataBaseAccess.Repositories.Contracts;

public interface IUserRepository
{
    public Task<List<UserEntity>> GetAll();
    public Task<List<UserEntity>>? GetAllWithFavMovies();
    public Task<List<UserEntity>>? GetAllWithWatchLaterMovies();
    public Task<UserEntity?> GetById(Guid id, bool trackChanges);
    public Task<UserEntity?> GetByEmail(string email);
    public Task<UserEntity?> GetByIdWithWatchLaterMovies(Guid? id);
    public Task AddAsync(UserEntity userEntity);
    public Task Update(UserEntity updatedUser, List<Guid>? FavMoviesIds, List<Guid>? WatchLaterMoviesIds);
    public Task AddWatchLaterMovies(Guid userId, List<MovieEntity> moviesAdded);
    public Task DeleteWatchLaterMovie(Guid userId, Guid movieId);
    public void DeleteUser(UserEntity user);
}