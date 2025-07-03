using Application.Dtos.GetDtos;

namespace Application.IServices;

public interface IGenreService
{
    Task<List<GenreDto>> GetAllAsync();
    Task<GenreDto> GetByIdAsync(Guid id);
    Task<GenreDto> GetByNameAsync(string name);
    Task<GenreDto> CreateAsync(string name);
    Task UpdateAsync(Guid id, string name);
    Task DeleteAsync(Guid id);
} 