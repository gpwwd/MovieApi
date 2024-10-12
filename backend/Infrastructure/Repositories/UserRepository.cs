using Application.IRepositories;
using Domain.Entities.MovieEntities;
using Domain.Entities.UsersEntities;
using Infrastructure.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository : RepositoryBase<UserEntity>, IUserRepository 
{
    public UserRepository(MovieDataBaseContext context) 
        : base(context)
    {
        
    }

    public async Task<List<UserEntity>> GetAllWithMovies()
    {
        return await FindAll(false)
            .Include(u => u.FavMovies)!
                .ThenInclude(m => m.Rating)
            .Include(u => u.WatchLaterMovies)!
                .ThenInclude(m => m.Rating)
            .AsSplitQuery()
            .ToListAsync();
    }
    
    public async Task<List<UserEntity>> GetAll()
    {
        return await FindAll(false)
            .ToListAsync();
    }
    
    public async Task<List<UserEntity>> GetAllWithFavMovies()
    {
        return await FindAll(false)
            .Include(u => u.FavMovies)
            .ToListAsync();
    }

    public async Task<List<UserEntity>> GetAllWithWatchLaterMovies()
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

    public async Task<ICollection<RoleEntity>?> GetRolesAsync(UserEntity userEntity)
    {
        var roleIds = _context.UserRoles
            .Where(u => u.UserId == userEntity.Id)
            .AsNoTracking()
            .Select(i => i.RoleId);
        
        return await _context.Roles
            .Where(u => roleIds.Contains(u.Id))
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<UserEntity?> GetByEmailAsync(string email)
    {
        return await FindAll(false)   
            .FirstOrDefaultAsync(u => u.Email == email);
    }
    
    public UserEntity? GetByEmail(string email)
    {
        return FindAll(false)   
            .FirstOrDefault(u => u.Email == email);
    }
    
    public async Task<UserEntity?> GetByNameAsync(string name, bool trackChanges)
    {
        return await FindAll(trackChanges)   
            .FirstOrDefaultAsync(u => u.UserName == name);
    }
    
    public UserEntity? GetByName(string name, bool trackChanges)
    {
        return FindAll(trackChanges)   
            .FirstOrDefault(u => u.UserName == name);
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
    
    public async Task AddToRolesAsync(UserEntity userEntity, ICollection<string>? roleNames)
    {
        var roles = await _context.Roles
            .Where(r => roleNames != null && roleNames.Contains(r.Name!)).ToListAsync();
        
        var userRoles = roles.Select(role => new IdentityUserRole<Guid>
        {
            UserId = userEntity.Id,
            RoleId = role.Id
        });
        
        _context.UserRoles.AddRange(userRoles); 
    }

    public async Task Update(UserEntity updatedUser, List<Guid>? FavMoviesIds, List<Guid>? WatchLaterMoviesIds)
    {
        var favMovies = await _context.Movies
            .Where(m => FavMoviesIds != null && FavMoviesIds.Contains(m.Id))
            .ToListAsync();
        
        var watchLaterMovies = await _context.Movies
            .Where(m => WatchLaterMoviesIds != null && WatchLaterMoviesIds.Contains(m.Id))
            .ToListAsync();
        
        updatedUser.FavMovies = favMovies;
        updatedUser.WatchLaterMovies = watchLaterMovies;
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

    public void DeleteWatchLaterMovie(UserEntity user, MovieEntity movie)
    {
        // _context.Entry(movie).State = EntityState.Unchanged;
        // _context.Entry(user).State = EntityState.Unchanged;
        if (user.WatchLaterMovies != null)
        {
            var movieToRemove = user.WatchLaterMovies
                .FirstOrDefault(m => m.Id == movie.Id);

            if (movieToRemove != null)
                user.WatchLaterMovies.Remove(movieToRemove);
        }
    }

    //<summary>
    // add async in delete methods
    //</summary>
    public void DeleteUser(UserEntity user)
    {
        Delete(user);
    }
}