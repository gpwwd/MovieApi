using Microsoft.EntityFrameworkCore;
using MovieApiMvc.DataBaseAccess.Entities.MovieEntities;
using MovieApiMvc.DataBaseAccess.Repositories.Contracts;
using MovieApiMvc.RequestFeatures;

namespace MovieApiMvc.DataBaseAccess.Repositories;

public class MovieRepository : RepositoryBase<MovieEntity>, IMovieRepository
{
    public MovieRepository(MovieDataBaseContext context)
        : base(context)
    {
    }
    
    public async Task<List<MovieEntity>> GetAll(bool trackChanges){
        return await FindAll(trackChanges)//get DbSet<T>
            .Include(m => m.Rating)
            .Include(m => m.Budget) 
            .Include(m => m.Genres)
            .Include(m => m.Countries)
            .AsSplitQuery()
            .ToListAsync();
    }
    
    public async Task<List<MovieEntity>> GetWithPaging(MovieParameters movieParams, bool trackChanges){
        return await FindAll(trackChanges)
            .Include(m => m.Rating)
            .Include(m => m.Budget)
            .Include(m => m.Genres)
            .Include(m => m.Countries)
            .Include(m => m.ImageInfoEntity)
            .AsSplitQuery()
            .OrderBy(m => m.Name)
            .Skip((movieParams.PageNumber - 1) * movieParams.PageSize)
            .Take(movieParams.PageSize)
            .ToListAsync();
    }
    
    public async Task<List<MovieEntity>> GetAllWithImages(bool trackChanges)
    {
        return await FindAll(trackChanges)
            .Include(m => m.Rating)
            .Include(m => m.Budget)
            .Include(m => m.Genres)
            .Include(m => m.Countries)
            .Include(m => m.ImageInfoEntity)
            .ToListAsync();
    }
    
    public async Task<MovieEntity?> GetById(Guid id, bool trackChanges){
        return await FindByCondition(m => m.Id == id, trackChanges)
            .Include(m => m.Rating)
            .Include(m => m.Budget)
            .Include(m => m.Genres)
            .Include(m => m.Countries)
            .Include(m => m.ImageInfoEntity)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task CreateMovie(MovieEntity movieEntity, IEnumerable<Guid> genresId,
        IEnumerable<Guid> countriesId, Guid? ratingId)
    {
        var genres = await _context.Genres.Where(g => genresId.Contains(g.Id)).ToListAsync();
        var countries = await _context.Countries.Where(c => countriesId.Contains(c.Id)).ToListAsync();
        var rating = await _context.Ratings.FirstOrDefaultAsync(r => r.Id == ratingId);

        movieEntity.Rating = rating;
        movieEntity.Genres = genres;
        movieEntity.Countries = countries;
        movieEntity.Id = Guid.NewGuid();

        if (movieEntity.Budget != null)
        {
            movieEntity.Budget.Id = Guid.NewGuid();
            movieEntity.Budget.MovieId = movieEntity.Id;
            _context.Budgets.Add(movieEntity.Budget); 
        }

        if (movieEntity.ImageInfoEntity != null)
        {
            movieEntity.ImageInfoEntity.Id = Guid.NewGuid();
            movieEntity.ImageInfoEntity.MovieId = movieEntity.Id;
            _context.Images.Add(movieEntity.ImageInfoEntity);
        }
        
        Create(movieEntity);
    }
    public void DeleteMovie(MovieEntity movieEntity)
    {
        Delete(movieEntity);
    }
}