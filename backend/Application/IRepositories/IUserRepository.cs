using Domain.Entities.MovieEntities;
using Domain.Entities.UsersEntities;

namespace Application.IRepositories;

public interface IUserRepository
{
    public Task<List<UserEntity>> GetAllWithMovies();
    public Task<List<UserEntity>> GetAll();
    public Task<List<UserEntity>>? GetAllWithFavMovies();
    public Task<List<UserEntity>>? GetAllWithWatchLaterMovies();
    public Task<UserEntity?> GetById(Guid id, bool trackChanges);
    public Task<ICollection<RoleEntity>?> GetRolesAsync(UserEntity userEntity);
    public Task<UserEntity?> GetByEmailAsync(string email);
    public UserEntity? GetByEmail(string email);
    public Task<UserEntity?> GetByNameAsync(string name, bool trackChanges);
    public UserEntity? GetByName(string name, bool trackChanges);
    public Task<UserEntity?> GetByIdWithWatchLaterMovies(Guid? id);
    public Task AddAsync(UserEntity userEntity);
    public Task AddToRolesAsync(UserEntity userEntity, ICollection<string>? roleNames);
    public Task Update(UserEntity updatedUser, List<Guid>? FavMoviesIds, List<Guid>? WatchLaterMoviesIds);
    public void AddWatchLaterMovies(UserEntity user, List<MovieEntity> moviesAdded);
    public void DeleteWatchLaterMovie(UserEntity user, MovieEntity movie);
    public void DeleteUser(UserEntity user);
}