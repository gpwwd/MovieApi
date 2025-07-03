using Application.Dtos.GetDtos;
using Application.Dtos.UpdateDtos;
using Application.IManagers;
using Application.IServices;
using AutoMapper;
using Domain.Entities.UsersEntities;
using Domain.Exceptions.NotFoundExceptions;

namespace Infrastructure.Services;

public class UsersService : IUsersService
{
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;
    
    public UsersService(IMapper mapper, IRepositoryManager repository)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<UserDto>> GetAll()
    {
        var users = await _repository.UserRepository.GetAll();
        var usersDto = _mapper.Map<List<UserDto>>(users);
        return usersDto;
    }
    
    public async Task<UserDto> GetById(Guid id)
    {
        var user = await _repository.UserRepository.GetById(id, false);
        return _mapper.Map<UserDto>(user);
    }
    
    public async Task<UserEntity?> GetEntityById(Guid id)
    {
        var user = await _repository.UserRepository.GetById(id, false);
        return user;
    }

    public async Task<UserEntity?> GetByNameAsync(string name)
    {
        var user = await _repository.UserRepository.GetByNameAsync(name, true);
        return user;
    }

    public UserEntity? GetByName(string name)
    {
        var user = _repository.UserRepository.GetByName(name, false);
        return user;
    }
    
    public async Task UpdateUser(Guid id, UserUpdateDto userDto)
    {
        var userEntity = await _repository.UserRepository.GetById(id, true);
        if(userEntity is null)
            throw new UserNotFoundException(id);
    
        _mapper.Map(userDto, userEntity);
        userEntity.PasswordHash = PasswordHasher.GeneratePasswordHash(userDto.Password);
        
        await _repository.UserRepository.Update(userEntity, userDto.FavMoviesIds, userDto.WatchLaterMoviesIds);
        await _repository.SaveAsync();
    }

    public async Task<UserEntity?> GetByEmailAsync(string email)
    {
        var user = await _repository.UserRepository.GetByEmailAsync(email);
        return user;
    }

    public UserEntity? GetByEmail(string email)
    {
        var user = _repository.UserRepository.GetByEmail(email);
        return user;
    }

    public async Task DeleteUser(Guid id)
    {
        var user = await _repository.UserRepository.GetById(id, false);
        if(user is null)
            throw new UserNotFoundException(id);
        _repository.UserRepository.DeleteUser(user);
        await _repository.SaveAsync();
    }

    public async Task<List<Guid>> AddToWatchLaterList(Guid userId, Guid[] moviesIds)
    {   
        var user = await _repository.UserRepository.GetById(userId, false);

        if(user is null)
            throw new UserNotFoundException(userId);
        
        var addedMovies = await _repository.MovieRepository
            .GetAll(false);
        addedMovies = addedMovies.Where(m => moviesIds.Contains(m.Id) &&
             (user.WatchLaterMovies == null || 
              !user.WatchLaterMovies.Select(w_m => w_m.Id).Contains(m.Id)))
            .ToList();
        
        if(!addedMovies.Any())
            throw new WatchLaterMoviesNotFound(moviesIds);
        
        _repository.UserRepository.AddWatchLaterMovies(user, addedMovies);
        await _repository.SaveAsync();
        
        return addedMovies.Select(m => m.Id).ToList();
    }   

    public async Task<List<Guid>> AddToFavMovies(Guid userId, Guid[] moviesIds)
    {
        var user = await _repository.UserRepository.GetById(userId, false);
        if(user is null)
            throw new UserNotFoundException(userId);

        var addedMovies = await _repository.MovieRepository
            .GetAll(false);
        addedMovies = addedMovies.Where(m => moviesIds.Contains(m.Id) &&
             (user.FavMovies == null || 
              !user.FavMovies.Select(f_m => f_m.Id).Contains(m.Id)))
            .ToList();

        if(!addedMovies.Any())
            throw new FavMoviesNotFound(moviesIds);

        _repository.UserRepository.AddFavMovies(user, addedMovies);
        await _repository.SaveAsync();

        return addedMovies.Select(m => m.Id).ToList();
    }

    public async Task RemoveWatchLaterUser(Guid userId, Guid movieId)
    {
        var user = await _repository.UserRepository.GetById(userId, true);
        if(user is null)
            throw new UserNotFoundException(userId);

        var deletedMovie = await _repository.MovieRepository.GetById(movieId, false);
        if(deletedMovie is null)
            throw new MovieNotFoundException(movieId);
        
        _repository.UserRepository.DeleteWatchLaterMovie(user, deletedMovie);
        await _repository.SaveAsync();
    }

    public async Task<List<MovieDto>> GetWatchLaterMovies(Guid userId)
    {   
        var user = await _repository.UserRepository.GetById(userId, false);
        
        if(user is null)
            throw new UserNotFoundException(userId);

        List<MovieDto> movieDtos = _mapper.Map<List<MovieDto>>(user.WatchLaterMovies);
        return movieDtos;
    }

}

/// <summary>
/// Инкапсулировать эту логику в классе пользвателя
/// </summary>
public static class PasswordHasher
{
    public static string GeneratePasswordHash(string password)
    {
        return password;
    }

    /// <summary>
    /// Использовать алгоритм хэширования
    /// <see cref="password"/> -> <see cref="passwordHash"/>>>
    /// </summary>
    /// <param name="password"></param>
    /// <param name="passwordHash"></param>
    /// <returns></returns>
    public static bool Verify(string password, string passwordHash)
    {
        return password == passwordHash;
    }
}