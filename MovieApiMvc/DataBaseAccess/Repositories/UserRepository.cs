using Microsoft.EntityFrameworkCore;
using MovieApiMvc.DataBaseAccess.Entities.MovieEntities;
using MovieApiMvc.DataBaseAccess.Entities.UsersEntities;
using MovieApiMvc.DataBaseAccess.Repositories.Contracts;

namespace MovieApiMvc.DataBaseAccess.Repositories;

public class UserRepository : RepositoryBase<UserEntity>, IUserRepository 
{
    public UserRepository(MovieDataBaseContext context) 
        : base(context)
    {
        
    }

    public async Task<List<UserEntity>> GetAll()
    {
        return await FindAll(false)
            .Include(u => u.FavMovies)!
                .ThenInclude(m => m.Rating)
            .Include(u => u.WatchLaterMovies)!
                .ThenInclude(m => m.Rating)
            .AsSplitQuery()
            .ToListAsync();
    }

    public async Task<List<UserEntity>>? GetAllWithFavMovies()
    {
        return await FindAll(false)
            .Include(u => u.FavMovies)
            .ToListAsync();
    }

    public async Task<List<UserEntity>>? GetAllWithWatchLaterMovies()
    {
        return await FindAll(false)
            .Include(u => u.WatchLaterMovies)
            .ToListAsync();
    }

    public async Task<UserEntity?> GetById(Guid id, bool trackChanges)
    {
        return await FindByCondition(u => u.Id == id, trackChanges)
            .Include(u => u.WatchLaterMovies)!
                .ThenInclude(m => m.Rating)
            .Include(u => u.WatchLaterMovies)!
                .ThenInclude(m => m.Countries)
            .Include(u => u.WatchLaterMovies)!
                .ThenInclude(m => m.Genres)
            .Include(u => u.WatchLaterMovies)!
                .ThenInclude(m => m.Budget)
            .Include(u => u.WatchLaterMovies)!
                .ThenInclude(m => m.ImageInfoEntity)
            .Include(u => u.FavMovies)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<UserEntity?> GetByEmail(string email)
    {
            return await FindAll(false)   
                .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<UserEntity?> GetByIdWithWatchLaterMovies(Guid? id)
    {
        return await FindAll(false)  
            .Include(u => u.WatchLaterMovies)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task AddAsync(UserEntity userEntity)
    {
        await CreateAsync(userEntity);
    }

    public async Task Update(UserEntity updatedUser, List<Guid>? FavMoviesIds, List<Guid>? WatchLaterMoviesIds)
    {
        var favMovies = await _context.Movies
            .Where(m => FavMoviesIds != null && FavMoviesIds.Contains(m.Id))
            .ToListAsync();
        
        var watchLaterMovies = await _context.Movies
            .Where(m => WatchLaterMoviesIds != null && WatchLaterMoviesIds.Contains(m.Id))
            .ToListAsync();
        
        var entries = _context.ChangeTracker.Entries();
        updatedUser.FavMovies = favMovies;
        updatedUser.WatchLaterMovies = watchLaterMovies;
        
        var entries2 = _context.ChangeTracker.Entries();
    }
    
    /// <summary>
    /// Привязывает добавляемые фильмы к контексту
    /// с статусом отслеживания Unchanged
    /// </summary>
    /// <param name="user"></param>
    /// <param name="moviesAdded"></param>
    public void AddWatchLaterMovies(UserEntity user, List<MovieEntity> moviesAdded)
    {
        foreach (var movie in moviesAdded)
            _context.Entry(movie).State = EntityState.Unchanged;
        _context.Entry(user).State = EntityState.Unchanged;
        
        if (user.WatchLaterMovies != null) 
            user.WatchLaterMovies.AddRange(moviesAdded);
        else
            user.WatchLaterMovies = moviesAdded;
    }

    public Task DeleteWatchLaterMovie(Guid userId, Guid movieId)
    {
        throw new NotImplementedException();
    }

    //<summary>
    // add async in delete methods
    //</summary>
    public void DeleteUser(UserEntity user)
    {
        Delete(user);
    }
}