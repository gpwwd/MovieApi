using AutoMapper;
using MovieApiMvc.DataBaseAccess.Repositories;
using MovieApiMvc.Services.Mappers;
using MovieApiMvc.Services.Interfaces;
using MovieApiMvc.DataBaseAccess.Entities.MovieEntities;
using MovieApiMvc.Models.DomainModels;
using MovieApiMvc.ErrorHandling;
using MovieApiMvc.Models.Dtos;
using MovieApiMvc.Models.Dtos.GetDtos;
using MovieApiMvc.RequestFeatures;

namespace MovieApiMvc.Services;

public class MoviesService : IMoviesService
{
    private readonly MoviesRepository _moviesRepository;
    private readonly IMapper _mapper;
    public MoviesService(MoviesRepository courseRepository, IMapper mapper)
    {
        _moviesRepository = courseRepository;
        _mapper = mapper;
    }

    public async Task<List<MovieDto>> GetAll()
    {
        var movies = await _moviesRepository.GetAll();
        List<MovieDto> moviesDto = _mapper.Map<List<MovieDto>>(movies);
        return moviesDto;
    }

    public async Task<List<MovieDto>> GetWithPaging(MovieParameters movieParams)
    {
        var movies = await _moviesRepository.GetWithPaging(movieParams);
        List<MovieDto> moviesDto = _mapper.Map<List<MovieDto>>(movies);
        return moviesDto;
    }

    public async Task<List<MovieDto>> GetAllWithImages()
    {
        var movies = await _moviesRepository.GetAllWithImages();
        List<MovieDto> moviesDto = _mapper.Map<List<MovieDto>>(movies);
        return moviesDto;
    }
    
    public async Task<MovieDto> GetById(Guid id)
    {
        var movie = await _moviesRepository.GetById(id);
        if (movie is null)
            throw new MovieNotFoundException(id);
        return _mapper.Map<MovieDto>(movie);
    }

    public async Task PutMovie(Guid id, MovieDto movieDto)
    {
        var movieEntity = await _moviesRepository.GetById(id);
        if(movieEntity is null)
                throw new MovieNotFoundException(id);
        _mapper.Map(movieDto, movieEntity);
        Console.WriteLine(movieEntity.Genres[0].Name);
        //call saving repo method
        //await _moviesRepository.Update(movieEntity);
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
            throw new ImageNotFoundException(id);
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
