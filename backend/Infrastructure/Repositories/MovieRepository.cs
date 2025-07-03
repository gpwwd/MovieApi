using Application.IRepositories;
using Application.RequestFeatures;
using Domain.Entities.MovieEntities;
using Infrastructure.Database;
using Infrastructure.Repositories.RepositoryExtensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class MovieRepository : RepositoryBase<MovieEntity>, IMovieRepository
{
    public MovieRepository(MovieDataBaseContext context)
        : base(context)
    {
    }
    
    public async Task<List<MovieEntity>> GetAll(bool trackChanges)
    {
        return await FindAll(trackChanges)
            .Include(m => m.Rating)
            .Include(m => m.Budget) 
            .Include(m => m.Genres)
            .Include(m => m.Countries)
            .AsSplitQuery()
            .ToListAsync();
    }
    
    public async Task<List<MovieEntity>> GetWithQuery(MovieRatingParameters movieRatingParams, bool trackChanges){
        return await FindByCondition(m => m.Rating != null, trackChanges)
            .FilterMovies(movieRatingParams.MinRating, movieRatingParams.MaxRating, movieRatingParams.RatingPlatform)
            .Include(m => m.Rating)
            .Include(m => m.Budget)
            .Include(m => m.Genres)
            .Include(m => m.Countries)
            .Include(m => m.ImageInfoEntity)
            .AsSplitQuery()
            .OrderBy(m => m.Name)
            .Skip((movieRatingParams.PageNumber - 1) * movieRatingParams.PageSize)
            .Take(movieRatingParams.PageSize)
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

    public async Task CreateMovie(MovieEntity movieEntity, List<string> genresNames, 
                              List<string> countriesNames)
    {
        var ratings = await _context.Ratings.AsNoTracking().ToListAsync();
        var rating = ratings.FirstOrDefault(r => r.Equals(movieEntity.Rating));
        
        movieEntity.Id = Guid.NewGuid();
        
        if (rating != null)
        {
            _context.Entry(rating).State = EntityState.Modified;
            movieEntity.Rating = rating; 
        }
        else if (movieEntity.Rating != null)
        {
            movieEntity.Rating.Id = Guid.NewGuid(); 
            _context.Ratings.Add(movieEntity.Rating); 
        }
        
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
        
        await _context.Movies.AddAsync(movieEntity);
        await _context.SaveChangesAsync(); 

        var genres = await _context.Genres
            .Where(g => genresNames.Contains(g.Name))
            .ToListAsync();
        
        var countries = await _context.Countries
            .Where(c => countriesNames.Contains(c.Name))
            .ToListAsync();
        
        movieEntity.Genres = genres;
        movieEntity.Countries = countries;
    }


    public async Task UpdateMovie(MovieEntity movieEntity, IEnumerable<string>? genresNames,
        IEnumerable<string>? countriesNames, short? Imdb, short? Kp, short? FilmCritics)
    {
        var genres = await _context.Genres
                                .Where(g => genresNames != null && genresNames.Contains(g.Name))
                                .ToListAsync();
        
        var countries = await _context.Countries
                                .Where(c => countriesNames != null && countriesNames.Contains(c.Name))
                                .ToListAsync();
        
        genres = genres.DistinctBy(g => g.Name).ToList();
        countries = countries.DistinctBy(c => c.Name).ToList();

        var tempRating = new RatingEntity();
        tempRating.Imdb = Imdb;
        tempRating.Kp = Kp;
        tempRating.FilmCritics = FilmCritics;
        var ratings = await _context.Ratings.AsNoTracking().ToListAsync();
        var rating = ratings.FirstOrDefault(r => r.Equals(tempRating));
        
        if(rating != null)
            _context.Entry(rating).State = EntityState.Modified;
        else
        {
            rating = new RatingEntity();
            rating = tempRating;
            _context.Ratings.Add(rating);
        }
        
        movieEntity.Rating = rating;
        movieEntity.Genres = genres;
        movieEntity.Countries = countries;
        _context.Entry(movieEntity).State = EntityState.Modified;
    }

    public void DeleteMovie(MovieEntity movieEntity)
    {
        Delete(movieEntity);
    }
}