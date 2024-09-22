using MovieApiMvc.DataBaseAccess.Repositories.Contracts;
using MovieApiMvc.Services.Interfaces;

namespace MovieApiMvc.DataBaseAccess.Repositories;

public class RepositoryManager : IRepositoryManager 
{
    private readonly MovieDataBaseContext _context;
    private readonly Lazy<IMovieRepository> _movieRepository;
    private readonly Lazy<IUserRepository> _userRepository;

    public RepositoryManager(MovieDataBaseContext repositoryContext)
    {
        _context = repositoryContext;
        _movieRepository = new Lazy<IMovieRepository>(new MovieRepository(repositoryContext));
        _userRepository = new Lazy<IUserRepository>(new UserRepository(repositoryContext));
    }

    public IMovieRepository MovieRepository => _movieRepository.Value; 
    public IUserRepository UserRepository => _userRepository.Value;

    public void Save() =>
        _context.SaveChanges();
}