using MovieApiMvc.DataBaseAccess.Repositories;
using MovieApiMvc.Services.Mappers;
using MovieApiMvc.Services.Interfaces;
using MovieApiMvc.DataBaseAccess.Entities.MovieEntities;
using MovieApiMvc.Models.DomainModels;
using MovieApiMvc.ErrorHandling;
using MovieApiMvc.Models.Dtos;
using MovieApiMvc.RequestFeatures;

namespace MovieApiMvc.Services;

public class MoviesService : IMoviesService
{
    private readonly MoviesRepository _moviesRepository;
    public MoviesService(MoviesRepository courseRepository)
    {
        _moviesRepository = courseRepository;
    }

    public async Task<List<MovieDto>> GetAll()
    {
        var movies = await _moviesRepository.GetAll();
        List<MovieDto> moviesDto = new List<MovieDto>();
        foreach(var movie in movies)
        {
            moviesDto.Add(EntityToDto.CreateMovieDtoFromEntity(movie));
        }
        return moviesDto;
    }

    public async Task<List<MovieDto>> GetWithPaging(MovieParameters movieParams)
    {
        var movies = await _moviesRepository.GetWithPaging(movieParams);
        List<MovieDto> moviesDto = new List<MovieDto>();
        foreach(var movie in movies)
        {
            moviesDto.Add(EntityToDto.CreateMovieDtoFromEntity(movie));
        }
        return moviesDto;
    }

    public async Task<List<MovieDto>> GetAllWithImages()
    {
        var movies = await _moviesRepository.GetAllWithImages();
        List<MovieDto> moviesDto = new List<MovieDto>();
        foreach(var movie in movies)
        {
            moviesDto.Add(EntityToDto.CreateMovieDtoFromEntity(movie));
        }
        return moviesDto;
    }
    
    public async Task<MovieDto> GetById(Guid id)
    {
        var movie = await _moviesRepository.GetById(id);
        if (movie is null)
            throw new MovieNotFoundException(id);
        return EntityToDto.CreateMovieDtoFromEntity(movie);//use mapper later
    }

    public async Task PutMovie(Guid id, MovieDto movieDto)
    {
        var movie = Movie.Create(
            name: movieDto.Name,
            type: movieDto.Type,
            year: movieDto.Year,
            rating: Rating.Create(movieId: id,
                                    kp: movieDto.RatingKp,
                                    imdb: movieDto.RatingImdb,
                                    filmCritics: movieDto.RatingFilmCritics
                                ),
            alternativeName: movieDto.AlternativeName,
            top250: movieDto.Top250
        );
        var movieEntity = ModelToEntity.CreateMovieEntityFromModel(movie);
        movieEntity.Id = id;
        await _moviesRepository.Update(movieEntity);
    }

    public async Task DeleteMovie(Guid id)
    {
        await _moviesRepository.Delete(id);
    }

    public async Task<MovieEntity> CreateMovie(MovieDto movieDto)
    {
        var movie = Movie.Create(
            name: movieDto.Name,
            type: movieDto.Type,
            year: movieDto.Year
        );
        var movieEntity = ModelToEntity.CreateMovieEntityFromModel(movie);
        await _moviesRepository.Add(movieEntity);
        return movieEntity;
    }

    public async Task<ImageInfoDto> GetImageById(Guid id)
    {
        var movie = await _moviesRepository.GetById(id);

        if(movie is null )
        {
            throw new MovieNotFoundException(id);
        }
        if(movie.ImageInfoEntity is null)
        {
            throw new EntityNotFoundException(404, "Not Found Image For Existing Movie");
        }

        var imageInfoEntity = movie.ImageInfoEntity;

        return new ImageInfoDto
        {
            Id = imageInfoEntity.Id,
            Urls = imageInfoEntity.Urls,
            PreviewUrls = imageInfoEntity.PreviewUrls
        };
    }
}
