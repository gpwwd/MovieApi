using Microsoft.EntityFrameworkCore;
using MovieApiMvc.ErrorHandling;
using MovieApiMvc.DataBaseAccess.Entities.MovieEntities;
using MovieApiMvc.DataBaseAccess.Entities.UsersEntities;
using MovieApiMvc.ErrorHandling.NotFoundExceptions;

namespace MovieApiMvc.DataBaseAccess.Repositories;

public class UsersRepository
{
    private readonly MovieDataBaseContext _dbContext;
    public UsersRepository(MovieDataBaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<UserEntity>> GetAll(){
        return await _dbContext.Users
            .AsNoTracking()
            .Include(u => u.FavMovies)
                .ThenInclude(m => m.Rating)
            .Include(u => u.WatchLaterMovies)
                .ThenInclude(m => m.Rating)
            .ToListAsync();
    }
    public async Task<List<UserEntity>>? GetAllWithFavMovies(){
        return await _dbContext.Users
            .AsNoTracking()
            .Include(u => u.FavMovies)
            .ToListAsync();
    }
    public async Task<List<UserEntity>>? GetAllWithWatchLaterMovies()
    {
        return await _dbContext.Users
            .AsNoTracking()
            .Include(u => u.WatchLaterMovies)
            .ToListAsync();
    }

    public async Task<UserEntity?> GetById(Guid id){
        return await _dbContext.Users   
            .AsNoTracking()
            .Include(u => u.WatchLaterMovies)
                .ThenInclude(m => m.Rating)
            .Include(u => u.WatchLaterMovies)
                .ThenInclude(m => m.Countries)
            .Include(u => u.WatchLaterMovies)
                .ThenInclude(m => m.Genres)
            .Include(u => u.WatchLaterMovies)
                .ThenInclude(m => m.Budget)
            .Include(u => u.WatchLaterMovies)
                .ThenInclude(m => m.ImageInfoEntity)
            .Include(u => u.FavMovies)
            .FirstOrDefaultAsync(u => u.Id == id);
    }
    
    public async Task<UserEntity?> GetByEmail(string email){
        return await _dbContext.Users   
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<UserEntity?> GetByIdWithWatchLaterMovies(Guid? id){
        return await _dbContext.Users   
            .AsNoTracking()
            .Include(u => u.WatchLaterMovies)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task Add(UserEntity userEntity)
    {
        await _dbContext.Users.AddAsync(userEntity);
        await _dbContext.SaveChangesAsync();                                          
    }

    public async Task Update(UserEntity updatedUser)
    {
        var existingUser = await _dbContext.Users
            .Include(u => u.FavMovies)
            .Include(u => u.WatchLaterMovies)
            .FirstOrDefaultAsync(u => u.Id == updatedUser.Id);

        if (existingUser == null)
        {
            throw new UserNotFoundException(updatedUser.Id);
        }

        existingUser.UserName = updatedUser.UserName;
        existingUser.PasswHash = updatedUser.PasswHash;
        existingUser.Email = updatedUser.Email;

        _dbContext.Users.Update(existingUser);
        await _dbContext.SaveChangesAsync();
    }

    public async Task AddWatchLaterMovies(Guid userId, List<MovieEntity> moviesAdded)
    {
        var existingUser = await _dbContext.Users
            .Include(u => u.WatchLaterMovies)
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (existingUser == null)
        {
            throw new UserNotFoundException(userId);
        }
    
        existingUser.WatchLaterMovies.AddRange(moviesAdded);

        var countries = existingUser.WatchLaterMovies
            .SelectMany(m => m.Countries)
            .Distinct();

        _dbContext.Countries.AttachRange(countries);

        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteWatchLaterMovie(Guid userId, Guid movieId)
    {
        var existingUser = await _dbContext.Users
            .Include(u => u.WatchLaterMovies)
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (existingUser == null)
        {
            throw new UserNotFoundException(userId);
        }

        var movie = existingUser.WatchLaterMovies?.FirstOrDefault(m => m.Id == movieId);

        if (movie == null)
        {
            throw new MovieNotFoundException(movieId);
        }

        existingUser.WatchLaterMovies.Remove(movie);

        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(Guid id){
        var user = await _dbContext.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
        _dbContext.Users.Remove(user);

        await _dbContext.SaveChangesAsync();
    }
}
