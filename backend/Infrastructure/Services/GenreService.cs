using Application.Dtos.GetDtos;
using Application.IManagers;
using Application.IServices;
using AutoMapper;
using Domain.Entities.MovieEntities;
using Domain.Exceptions;

namespace Infrastructure.Services;

public class GenreService : IGenreService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;

    public GenreService(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
    }

    public async Task<List<GenreDto>> GetAllAsync()
    {
        var genres = await _repositoryManager.GenreRepository.GetAllAsync();
        return _mapper.Map<List<GenreDto>>(genres);
    }

    public async Task<GenreDto> GetByIdAsync(Guid id)
    {
        var genre = await _repositoryManager.GenreRepository.GetByIdAsync(id);
        return _mapper.Map<GenreDto>(genre);
    }

    public async Task<GenreDto> GetByNameAsync(string name)
    {
        var genre = await _repositoryManager.GenreRepository.GetByNameAsync(name);
        return _mapper.Map<GenreDto>(genre);
    }

    public async Task<GenreDto> CreateAsync(string name)
    {
        if (await _repositoryManager.GenreRepository.ExistsByNameAsync(name))
            throw new EntityAlreadyExistsException($"Genre with name '{name}' already exists");

        var genre = new GenreEntity
        {
            Id = Guid.NewGuid(),
            Name = name
        };

        await _repositoryManager.GenreRepository.CreateAsync(genre);
        await _repositoryManager.SaveAsync();

        return _mapper.Map<GenreDto>(genre);
    }

    public async Task UpdateAsync(Guid id, string name)
    {
        if (!await _repositoryManager.GenreRepository.ExistsAsync(id))
            throw new EntityNotFoundException($"Genre with id '{id}' not found");

        if (await _repositoryManager.GenreRepository.ExistsByNameAsync(name))
            throw new EntityAlreadyExistsException($"Genre with name '{name}' already exists");

        var genre = new GenreEntity
        {
            Id = id,
            Name = name
        };

        await _repositoryManager.GenreRepository.UpdateAsync(genre);
        await _repositoryManager.SaveAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        await _repositoryManager.GenreRepository.DeleteAsync(id);
        await _repositoryManager.SaveAsync();
    }
} 