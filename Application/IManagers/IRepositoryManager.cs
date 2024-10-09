using Application.IRepositories;

namespace Application.IManagers;

public interface IRepositoryManager
{
    IMovieRepository MovieRepository { get; }
    IUserRepository UserRepository { get; }
    ICountriesRepository CountriesRepository { get; }
    Task SaveAsync();
}