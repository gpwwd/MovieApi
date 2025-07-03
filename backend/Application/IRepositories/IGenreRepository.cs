using Domain.Entities.MovieEntities;

namespace Application.IRepositories;

public interface IGenreRepository
{
    Task<List<GenreEntity>> GetAllAsync();
    Task<GenreEntity> GetByIdAsync(Guid id);
    Task<GenreEntity> GetByNameAsync(string name);
    Task<GenreEntity> CreateAsync(GenreEntity genre);
    Task UpdateAsync(GenreEntity genre);
    Task DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
    Task<bool> ExistsByNameAsync(string name);
} 