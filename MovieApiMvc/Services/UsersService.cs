using MovieApiMvc.DataBaseAccess.Repositories;
using MovieApiMvc.Services.Interfaces;
using MovieApiMvc.Services.Mappers;
using MovieApiMvc.ErrorHandling;
using MovieApiMvc.Models.Dtos;
using MovieApiMvc.DataBaseAccess.Entities.MovieEntities;
using MovieApiMvc.DataBaseAccess.Entities.MovieEntities.UsersEntities;
using MovieApiMvc.Models.Dtos.GetDtos;

namespace MovieApiMvc.Services;

public class UsersService : IUsersService
{
    private readonly UsersRepository _usersRepository;
    private readonly MoviesRepository _moviesRepository;
    private readonly IJwtProvider _jwtProvider;
    public UsersService(UsersRepository usersRepository, MoviesRepository moviesRepository, IJwtProvider jwtProvider)
    {
        _usersRepository = usersRepository;
        _moviesRepository = moviesRepository; 
        _jwtProvider = jwtProvider;
    }

    public async Task<List<UserDto>> GetAll()
    {
        var users = await _usersRepository.GetAll();
        List<UserDto> usersDto = new List<UserDto>();
        foreach(var user in users)
        {
            usersDto.Add(EntityToDto.CreateUserDtoFromEntity(user));
        }
        return usersDto;
    }
    public async Task<UserDto> GetById(Guid id)
    {
        var user = await _usersRepository.GetById(id);
        return EntityToDto.CreateUserDtoFromEntity(user);
    }
    public async Task<UserEntity> CreateUser(UserDto userDto)
    {
        var userEntity = DtoToEntity.CreateUserEntityFromDto(userDto, userDto.Password);
        await _usersRepository.Add(userEntity);
        return userEntity;
    }

    public async Task<UserEntity> Register(UserDto userDto)
    {
        var passwHash = PasswordHasher.GeneratePasswHash(userDto.Password);

        var userEntity = DtoToEntity.CreateUserEntityFromDto(userDto, passwHash);

        await _usersRepository.Add(userEntity);
        return userEntity;
    }

    public async Task<string> Login(UserLoginDto userDto)
    {
        var userEntity = await _usersRepository.GetByEmail(userDto.Email);

        if(userEntity is null)
        {
            throw new MyExeption(401, "Not registred");
        }

        var IsPasswCorr = PasswordHasher.Verify(userDto.Password, userEntity.PasswHash);
        
        if(!IsPasswCorr)
        {
            throw new MyExeption(401, "Incorrect passw");
        }

        var token = _jwtProvider.GenerateToken(userDto, userEntity.Id);
        // If the identifier and secret are valid, the app can set the principal for the
        // current request, but it also needs a way of storing these details for
        // subsequent requests. For traditional web apps, this is typically achieved
        // by storing an encrypted version of the user principal in a cookie.
        return token;
    }   
    
    public async Task UpdateUser(UserDto user)
    {
        UserEntity? userEntity = await _usersRepository.GetById(user.Id);

        if(userEntity is null)
        {
            throw new UserNotFoundException(user.Id);
        }

        await _usersRepository.Update(userEntity);
    }

    public async Task DeleteUser(Guid id)
    {
        await _usersRepository.Delete(id);
    }

    public async Task<List<Guid>> AddToWatchLaterList(Guid userId, Guid[] moviesIds)
    {   
        var user = await _usersRepository.GetById(userId);

        if(user is null)
        {
            throw new UserNotFoundException(userId);
        }

        List<Guid> moviesAddedIds = new List<Guid>();
        List<MovieEntity> moviesAdded = new List<MovieEntity>();
        foreach(var movieId in moviesIds)
        {
            var movie = await _moviesRepository.GetById(movieId);

            if(movie is null)
            {
                continue;
            }

            MovieEntity? wasAlreadyAddedMovie = user.WatchLaterMovies.FirstOrDefault(m => m.Id == movieId);
            if(!(wasAlreadyAddedMovie is null))
            {
                throw new EntityAlreadyExistException(409, "Watch Later Movie With This Id Already Exists");
            }
            
            moviesAdded.Add(movie);
            moviesAddedIds.Add(movieId);
        }

        await _usersRepository.AddWatchLaterMovies(userId, moviesAdded);  
        return moviesAddedIds;
    }   

    public async Task RemoveWatchLaterUser(Guid userId, Guid movieId)
    {
        await _usersRepository.DeleteWatchLaterMovie(userId, movieId);
    }

    public async Task<List<MovieDto>> GetWatchLaterMovies(Guid userId)
    {   
        var user = await _usersRepository.GetById(userId);
        
        if(user is null)
        {
            throw new UserNotFoundException(userId);
        }

        List<MovieDto> movieDtos = new List<MovieDto>();
        var movies = user.WatchLaterMovies;
        foreach(var movie in movies)
        {   
            var movieDto = EntityToDto.CreateMovieDtoFromEntity(movie); 
            movieDtos.Add(movieDto);
        }
        return movieDtos;
    }

}

//remove to domain model later
public static class PasswordHasher
{
    public static string GeneratePasswHash(string password)
    {
        return password;
    }

    public static bool Verify(string password, string passwHash)
    {
        return password == passwHash;
    }
}