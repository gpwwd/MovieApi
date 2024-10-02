using AutoMapper;
using MovieApiMvc.Services.Interfaces;
using MovieApiMvc.Models.Dtos;
using MovieApiMvc.DataBaseAccess.Entities.UsersEntities;
using MovieApiMvc.ErrorHandling.AuthenticationExtensions;
using MovieApiMvc.ErrorHandling.NotFoundExceptions;
using MovieApiMvc.Models.Dtos.GetDtos;
using MovieApiMvc.Models.Dtos.PostDtos;
using MovieApiMvc.Models.Dtos.UpdateDtos;

namespace MovieApiMvc.Services;

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
    
    public async Task<UserDto> GetByName(string name)
    {
        var user = await _repository.UserRepository.GetByName(name);
        return _mapper.Map<UserDto>(user);
    }

    /// <summary>
    /// 
    /// move token from local storage to cookies on frontend
    /// /////////////////////////////////////////////////////////////////////////////////
    /// If the identifier and secret are valid, the app can set the principal for the
    /// current request, but it also needs a way of storing these details for
    /// subsequent requests. For traditional web apps, this is typically achieved
    /// by storing an encrypted version of the user principal in a cookie.
    /// </summary>
    
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

    ///<summary>
    /// add async in delete methods
    ///</summary>
    public async Task DeleteUser(Guid id)
    {
        var user = await _repository.UserRepository.GetById(id, false);
        if(user is null)
            throw new UserNotFoundException(id);
        _repository.UserRepository.DeleteUser(user);
        await _repository.SaveAsync();
    }

    /// <summary>
    /// Добавляемые фильмы проверяются на уникальность
    /// путем проверки их вхождения в текцщий список фильмов пользователя
    /// <see>
    ///     <cref>(user.WatchLaterMovies == null || !user.WatchLaterMovies.Select(w_m == w_m.Id).Contains(m.Id)))</cref>
    /// </see>
    /// Метод репозитория <AddWatchLaterMovies/>> отслеживает сущности и добавляет пользователю фильмы
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="moviesIds"></param>
    /// <returns>Id добавленных пользователей </returns>
    /// <exception cref="UserNotFoundException"></exception>
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