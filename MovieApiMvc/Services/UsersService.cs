using AutoMapper;
using MovieApiMvc.DataBaseAccess.Repositories;
using MovieApiMvc.Services.Interfaces;
using MovieApiMvc.Services.Mappers;
using MovieApiMvc.ErrorHandling;
using MovieApiMvc.Models.Dtos;
using MovieApiMvc.DataBaseAccess.Entities.MovieEntities;
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
    private readonly UsersRepository _usersRepository;
    private readonly MoviesRepository _moviesRepository;
    private readonly IJwtProvider _jwtProvider;
    private readonly IMapper _mapper;
    public UsersService(UsersRepository usersRepository, MoviesRepository moviesRepository,
        IJwtProvider jwtProvider, IMapper mapper, IRepositoryManager repository)
    {
        _repository = repository;
        _mapper = mapper;
        _usersRepository = usersRepository;
        _moviesRepository = moviesRepository; 
        _jwtProvider = jwtProvider;
    }

    public async Task<List<UserDto>> GetAll()
    {
        var users = await _repository.UserRepository.GetAll();
        var usersDto = _mapper.Map<List<UserDto>>(users);
        //<summury> test with innerconnected entities </summury>
        return usersDto;
    }
    public async Task<UserDto> GetById(Guid id)
    {
        var user = await _repository.UserRepository.GetById(id, false);
        return _mapper.Map<UserDto>(user);
    }
    public async Task<UserDto> CreateUser(UserForRegistrationDto userDto)
    {
        var userEntity = _mapper.Map<UserEntity>(userDto);
        userEntity.PasswHash = PasswordHasher.GeneratePasswHash(userDto.Password);
        
        await _repository.UserRepository.AddAsync(userEntity);
        await _repository.SaveAsync();
        
        var userToReturn = _mapper.Map<UserDto>(userEntity);
        return userToReturn;
    }

    public async Task<UserDto> Register(UserDto userDto)
    {
        var userEntity = _mapper.Map<UserEntity>(userDto);
        userEntity.PasswHash = PasswordHasher.GeneratePasswHash(userDto.Password);
        
        await _repository.UserRepository.AddAsync(userEntity);
        await _repository.SaveAsync();
        
        var userToReturn = _mapper.Map<UserDto>(userEntity);
        return userToReturn;
    }

    public async Task<string> Login(UserLoginDto userDto)
    {
        var userEntity = await _repository.UserRepository.GetByEmail(userDto.Email);
        
        if(userEntity is null)
            throw new NotRegistredException(userDto.Email);
        
        var IsPasswordCorrect = PasswordHasher.Verify(PasswordHasher.GeneratePasswHash(userDto.Password), userEntity.PasswHash);
        
        if(!IsPasswordCorrect)
            throw new MyExeption(401, "Incorrect passw");

        var token = _jwtProvider.GenerateToken(userDto, userEntity.Id);
        // If the identifier and secret are valid, the app can set the principal for the
        // current request, but it also needs a way of storing these details for
        // subsequent requests. For traditional web apps, this is typically achieved
        // by storing an encrypted version of the user principal in a cookie.
        
        ////////////////////////////////////////////////////////////////////////////////
        // <summary>
        // move token from local storage to cookies
        // </summary>
        return token;
    }   
    
    public async Task UpdateUser(UserUpdateDto userDto)
    {
        var userEntity = await _repository.UserRepository.GetById(userDto.Id, true);
        if(userEntity is null)
            throw new UserNotFoundException(userDto.Id);
    
        _mapper.Map(userDto, userEntity);
        userEntity.PasswHash = PasswordHasher.GeneratePasswHash(userDto.Password);
        
        await _repository.UserRepository.Update(userEntity, userDto.FavMoviesIds, userDto.WatchLaterMoviesIds);
        await _repository.SaveAsync();
    }

    //<summary>
    // add async in delete methods
    //</summary>
    public async Task DeleteUser(Guid id)
    {
        var user = await _repository.UserRepository.GetById(id, false);
        if(user is null)
            throw new UserNotFoundException(id);
        _repository.UserRepository.DeleteUser(user);
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