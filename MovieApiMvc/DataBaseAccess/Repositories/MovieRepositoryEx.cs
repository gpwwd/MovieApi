using Microsoft.EntityFrameworkCore;
using MovieApiMvc.DataBaseAccess.Entities.MovieEntities;
using MovieApiMvc.ErrorHandling;
using MovieApiMvc.ErrorHandling.NotFoundExceptions;
using MovieApiMvc.ExternalApi;
using MovieApiMvc.RequestFeatures;

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
            .AsSplitQuery()
            .ToListAsync();
    }

    public async Task<List<MovieEntity>> GetWithPaging(MovieParameters movieParams){
        return await _dbContext.Movies
            .AsNoTracking()
            .Include(m => m.Rating)
            .Include(m => m.Budget)
            .Include(m => m.Genres)
            .Include(m => m.Countries)
            .OrderBy(m => m.Name)
            .Skip((movieParams.PageNumber - 1) * movieParams.PageSize)
            .Take(movieParams.PageSize)
            .ToListAsync();
    }

    public async Task<List<MovieEntity>> GetAllWithImages()
    {
        return await _dbContext.Movies
            .AsNoTracking()
            .Include(m => m.Rating)
            .Include(m => m.Budget)
            .Include(m => m.Genres)
            .Include(m => m.Countries)
            .Include(m => m.ImageInfoEntity)
            .ToListAsync();
    }
    public async Task<MovieEntity?> GetById(Guid id){
        return await _dbContext.Movies
            .AsNoTracking()
            .Include(m => m.Rating)
            .Include(m => m.Budget)
            .Include(m => m.Genres)
            .Include(m => m.Countries)
            .Include(m => m.ImageInfoEntity)
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
                                        .Include(m => m.ImageInfoEntity)
                                        .FirstOrDefaultAsync(m => m.Id == movie.Id);

        if(movieEntity is null)
        {
            throw new MovieNotFoundException(movie.Id);
        }

        movieEntity.Name = movie.Name;
        movieEntity.AlternativeName = movie.AlternativeName;
        movieEntity.Type = movie.Type;
        movieEntity.Year = movie.Year;
        //to solve proplem with overwriting id
        movieEntity.Rating.Imdb = movie.Rating.Imdb;
        movieEntity.Rating.Kp = movie.Rating.Kp;
        movieEntity.Rating.RussianFilmCritics = movie.Rating.RussianFilmCritics;
        movieEntity.Rating.FilmCritics = movie.Rating.FilmCritics;
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

    public async Task<Guid?> GetIdByName(string name){
        var movie = await _dbContext.Movies
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Name == name);

        if(movie is null)
        {
            throw new MovieNotFoundException(movie.Id);
        }

        return movie.Id;
    }

    public async Task PutPoster(Guid? id, ImageInfo image)
    {
        var movieEntity = await _dbContext.Movies.Include(m => m.ImageInfoEntity)
                                        .FirstOrDefaultAsync(m => m.Id == id);

        if (movieEntity == null)
        {
            throw new Exception("Movie not found");
        }

        if (movieEntity.ImageInfoEntity == null)
        {   
            try
            {
                var imageInfoEntity = new ImageInfoEntity
                {
                    Id = Guid.NewGuid(),
                    Urls = image.Urls,
                    PreviewUrls = image.PreviewUrls,
                    MovieId = id ?? throw new MyExeption(404, "no id")
                };
                _dbContext.Images.Add(imageInfoEntity);
            }
            catch(MyExeption ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        else
        {
            // Обновляем существующие значения
            movieEntity.ImageInfoEntity.Urls = image.Urls;
            movieEntity.ImageInfoEntity.PreviewUrls = image.PreviewUrls;
            _dbContext.Images.Update(movieEntity.ImageInfoEntity);
        }

        await _dbContext.SaveChangesAsync();
    }

    public async Task PutDescription(Guid? id, string description, string shortDescription)
    {
        var movieEntity = await _dbContext.Movies
                                        .FirstOrDefaultAsync(m => m.Id == id);

        if (movieEntity == null)
        {
            throw new Exception("Movie not found");
        }

        movieEntity.Description = description;
        movieEntity.ShortDescription = shortDescription;
        //_dbContext.Movies.Update(movieEntity);


        await _dbContext.SaveChangesAsync();
    }
}
