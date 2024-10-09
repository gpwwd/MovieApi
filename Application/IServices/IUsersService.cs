using Application.Dtos.GetDtos;
using Application.Dtos.UpdateDtos;
using Domain.Entities.UsersEntities;

namespace Application.IServices;

public interface IUsersService
{
    public Task<List<UserDto>> GetAll();
    public Task<UserDto> GetById(Guid id);
    public Task<UserEntity?> GetByNameAsync(string name);
    public Task DeleteUser(Guid id);
    public Task<List<Guid>> AddToWatchLaterList(Guid userId, Guid[] moviesIds);
    public Task RemoveWatchLaterUser(Guid userId, Guid movieId);
    public Task<List<MovieDto>> GetWatchLaterMovies(Guid userId);
    public Task UpdateUser(Guid id, UserUpdateDto user);
}
