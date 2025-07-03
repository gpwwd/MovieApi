using Application.IRepositories;
using Domain.Entities.MovieEntities;
using Domain.Exceptions.NotFoundExceptions;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class GenreRepository : IGenreRepository
{
    private readonly MovieDataBaseContext _context;

    public GenreRepository(MovieDataBaseContext context)
    {
        _context = context;
    }

    public async Task<List<GenreEntity>> GetAllAsync()
    {
        return await _context.Genres
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<GenreEntity> GetByIdAsync(Guid id)
    {
        var genre = await _context.Genres.FindAsync(id);
        if (genre == null)
            throw new GenreNotFoundException(id);
        return genre;
    }

    public async Task<GenreEntity> GetByNameAsync(string name)
    {
        var genre = await _context.Genres
            .FirstOrDefaultAsync(g => g.Name.ToLower() == name.ToLower());
        if (genre == null)
            throw new GenreNotFoundException(name);
        return genre;
    }

    public async Task<GenreEntity> CreateAsync(GenreEntity genre)
    {
        await _context.Genres.AddAsync(genre);
        return genre;
    }

    public async Task UpdateAsync(GenreEntity genre)
    {
        var existingGenre = await GetByIdAsync(genre.Id);
        _context.Entry(existingGenre).CurrentValues.SetValues(genre);
    }

    public async Task DeleteAsync(Guid id)
    {
        var genre = await GetByIdAsync(id);
        _context.Genres.Remove(genre);
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.Genres.AnyAsync(g => g.Id == id);
    }

    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await _context.Genres.AnyAsync(g => g.Name.ToLower() == name.ToLower());
    }
} 