using Microsoft.EntityFrameworkCore;
using MovieApiMvc.DataBaseAccess.Entities.MovieEntities;
using MovieApiMvc.DataBaseAccess.Repositories.Contracts;
using MovieApiMvc.ErrorHandling;
using MovieApiMvc.Models.Dtos.GetDtos;
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

    public async Task CreateMovie(MovieEntity movieEntity, List<string> genresNames, 
        List<string> countriesNames, Guid? ratingId)
    {
        var genres = await _context.Genres.Where(g => genresNames.Contains(g.Name)).ToListAsync();
        var countries = await _context.Countries.Where(c => countriesNames.Contains(c.Name)).ToListAsync();
        var rating = await _context.Ratings.FirstOrDefaultAsync(r => r.Id == ratingId);//to do search by name not id
        
        genres = genres.DistinctBy(g => g.Name).ToList();
        countries = countries.DistinctBy(c => c.Name).ToList();
        
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

    public async Task UpdateMovie(MovieEntity movieEntity, IEnumerable<string>? genresNames,
        IEnumerable<string>? countriesNames, Guid? ratingId)
    {   
        var genres = await _context.Genres
                                .Where(g => genresNames != null && genresNames.Contains(g.Name))
                                .ToListAsync();
        
        var countries = await _context.Countries
                                .Where(c => countriesNames != null && countriesNames.Contains(c.Name))
                                .ToListAsync();
        var rating = await _context.Ratings.FirstOrDefaultAsync(r => r.Id == ratingId);//to do search by name not id
        
        genres = genres.DistinctBy(g => g.Name).ToList();
        countries = countries.DistinctBy(c => c.Name).ToList();
        
        movieEntity.Rating = rating;
        movieEntity.Genres = genres;
        movieEntity.Countries = countries;
    }
    // public async Task PutPoster(Guid id, ImageInfoDto image)
    // {
    //     var movieEntity = await FindByCondition(m => m.Id == id, true)
    //         .Include(m => m.ImageInfoEntity)
    //         .FirstOrDefaultAsync(m => m.Id == id);
    //
    //     if (movieEntity == null)
    //         throw new MovieNotFoundException(id);
    //
    //     
    //     if (movieEntity.ImageInfoEntity == null)
    //     {   
    //         try
    //         {
    //             var imageInfoEntity = new ImageInfoEntity
    //             {
    //                 Id = Guid.NewGuid(),
    //                 Urls = image.Urls,
    //                 PreviewUrls = image.PreviewUrls,
    //                 //MovieId = id ?? throw new MyExeption(404, "no id")
    //             };
    //             //_dbContext.Images.Add(imageInfoEntity);
    //         }
    //         catch(MyExeption ex)
    //         {
    //             Console.WriteLine(ex.Message);
    //         }
    //
    //     }
    //     else
    //     {
    //         // Обновляем существующие значения
    //         movieEntity.ImageInfoEntity.Urls = image.Urls;
    //         movieEntity.ImageInfoEntity.PreviewUrls = image.PreviewUrls;
    //         //_dbContext.Images.Update(movieEntity.ImageInfoEntity);
    //     }
    //
    //     //await _dbContext.SaveChangesAsync();
    // }
    public void DeleteMovie(MovieEntity movieEntity)
    {
        Delete(movieEntity);
    }
}