using MovieApiMvc.Dtos;
using MovieApiMvc.DataBaseAccess.Repositories;
using MovieApiMvc.Services.Mappers;
using MovieApiMvc.Services.Interfaces;
using MovieApiMvc.DataBaseAccess.Entities;
using MovieApiMvc.Models.DomainModels;

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
    
    public async Task<MovieDto> GetById(Guid id)
    {
        var movie = await _moviesRepository.GetById(id);
        return EntityToDto.CreateMovieDtoFromEntity(movie);
    }

    public async Task PutMovie(Guid id, MovieDto movieDto)
    {
        var movie = Movie.Create(
            name: movieDto.Name,
            type: movieDto.Type,
            year: movieDto.Year,
            rating: Rating.Create(movieId: id,
                                 kp: movieDto.Rate)
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
}
