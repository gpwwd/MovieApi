using Application.IManagers;
using Application.IRepositories;
using Infrastructure.Database;
using Infrastructure.Repositories;

namespace Infrastructure.Managers;

public class RepositoryManager : IRepositoryManager 
{
    private readonly MovieDataBaseContext _context;
    private readonly Lazy<IMovieRepository> _movieRepository;
    private readonly Lazy<IUserRepository> _userRepository;
    private readonly Lazy<ICountriesRepository> _countriesRepository;
    private readonly Lazy<IGenreRepository> _genreRepository;
    
    public RepositoryManager(MovieDataBaseContext repositoryContext)
    {
        _context = repositoryContext;
        _movieRepository = new Lazy<IMovieRepository>(new MovieRepository(repositoryContext));
        _userRepository = new Lazy<IUserRepository>(new UserRepository(repositoryContext));
        _countriesRepository = new Lazy<ICountriesRepository>(new CountriesRepository(repositoryContext));
        _genreRepository = new Lazy<IGenreRepository>(new GenreRepository(repositoryContext));
    }

    public IMovieRepository MovieRepository => _movieRepository.Value; 
    public IUserRepository UserRepository => _userRepository.Value;
    public ICountriesRepository CountriesRepository => _countriesRepository.Value;
    public IGenreRepository GenreRepository => _genreRepository.Value;

    public async Task SaveAsync() =>
        await _context.SaveChangesAsync();
}