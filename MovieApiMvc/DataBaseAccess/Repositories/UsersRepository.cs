using MovieApiMvc.DataBaseAccess.Context;
using Microsoft.EntityFrameworkCore;
using MovieApiMvc.DataBaseAccess.Entities.UsersEntities;
using Microsoft.AspNetCore.Mvc;
using MovieApiMvc.ErrorHandling;

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
            throw new EntityNotFoundException(404, "User not found");
        }

        existingUser.UserName = updatedUser.UserName;
        existingUser.PasswHash = updatedUser.PasswHash;
        existingUser.Email = updatedUser.Email;
        
        _dbContext.Users.Attach(existingUser);
        await _dbContext.SaveChangesAsync();
    }
    public async Task Delete(Guid id){
        var user = await _dbContext.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
        _dbContext.Users.Remove(user);

        await _dbContext.SaveChangesAsync();
    }
}
