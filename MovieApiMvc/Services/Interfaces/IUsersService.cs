using MovieApiMvc.DataBaseAccess.Entities;
using MovieApiMvc.DataBaseAccess.Entities.MovieEntities.UsersEntities;
using MovieApiMvc.Models.Dtos;
using MovieApiMvc.Models.Dtos.GetDtos;

namespace MovieApiMvc.Services.Interfaces;

public interface IUsersService
{
    public Task<List<UserDto>> GetAll();
    public Task<UserEntity> CreateUser(UserDto userDto);
    public Task<UserDto> GetById(Guid id);
    public Task DeleteUser(Guid id);
    public Task<List<Guid>> AddToWatchLaterList(Guid userId, Guid[] moviesIds);
    public Task RemoveWatchLaterUser(Guid userId, Guid movieId);
    public Task<List<MovieDto>> GetWatchLaterMovies(Guid userId);
    public Task<UserEntity> Register(UserDto userDto);   
    public Task<string> Login(UserLoginDto userLoginDto);
    public Task UpdateUser(UserDto user);
}
