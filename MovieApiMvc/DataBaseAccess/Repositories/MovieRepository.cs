using Microsoft.EntityFrameworkCore;
using MovieApiMvc.DataBaseAccess.Entities;
using MovieApiMvc.DataBaseAccess.Context;
using Microsoft.VisualBasic;
using MovieApiMvc.ErrorHandling;

namespace MovieApiMvc.DataBaseAccess.Repositories;

public class MoviesRepository
{

    private readonly MovieDataBaseContext _dbContext;
    public MoviesRepository(MovieDataBaseContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<List<MovieEntity>> GetAll(){
        return await _dbContext.Movies
            .AsNoTracking()
            .Include(m => m.Rating)
            .Include(m => m.Budget)
            .Include(m => m.Genres)
            .Include(m => m.Countries)
            .ToListAsync();
    }

    public async Task<MovieEntity?> GetById(Guid id){
        return await _dbContext.Movies
            .AsNoTracking()
            .Include(m => m.Rating)
            .Include(m => m.Budget)
            .Include(m => m.Genres)
            .Include(m => m.Countries)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<MovieEntity> Add(MovieEntity movieEntity)
    {
        await _dbContext.Ratings.AddAsync(movieEntity.Rating);
        await _dbContext.Movies.AddAsync(movieEntity);
        await _dbContext.SaveChangesAsync();     
        return movieEntity;                                     
    }
    public async Task Update(MovieEntity movie)
    {
        var movieEntity = await _dbContext.Movies.Include(m => m.Rating)
                                        .Include(m => m.Budget)
                                        .Include(m => m.Countries)
                                        .Include(m => m.Genres)
                                        .FirstOrDefaultAsync(m => m.Id == movie.Id);

        if(movieEntity is null)
        {
            throw new EntityNotFoundException(404, "Movie to update not found");
        }

        movieEntity.Name = movie.Name;
        movieEntity.AlternativeName = movie.AlternativeName;
        movieEntity.Type = movie.Type;
        movieEntity.Year = movie.Year;
        //to solve proplem with overwriting id
        movieEntity.Rating.imdb = movie.Rating.imdb;
        movieEntity.Rating.kp = movie.Rating.kp;
        movieEntity.Rating.russianFilmCritics = movie.Rating.russianFilmCritics;
        movieEntity.Rating.filmCritics = movie.Rating.filmCritics;
        movieEntity.MovieLength = movie.MovieLength;
        movieEntity.Genres = movie.Genres;
        movieEntity.Countries = movie.Countries;
        movieEntity.Budget = movie.Budget;
        movieEntity.Top250 = movie.Top250;
        movieEntity.IsSeries = movie.IsSeries;

        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(Guid id){
        var movie = await _dbContext.Movies.Where(m => m.Id == id).FirstOrDefaultAsync();

        if(movie == null)
        {
            return;
        }

        _dbContext.Movies.Remove(movie);

        await _dbContext.SaveChangesAsync();
    }
}
