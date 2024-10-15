using Application.Dtos.GetDtos;
using Application.Dtos.PostDtos;
using Application.Dtos.UpdateDtos;
using Application.IManagers;
using Application.IServices;
using Application.RequestFeatures;
using AutoMapper;
using Domain.Entities.MovieEntities;
using Domain.Exceptions.NotFoundExceptions;

namespace Infrastructure.Services;

public class MoviesService : IMoviesService
{
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;
    public MoviesService(IRepositoryManager courseRepository, IMapper mapper)
    {
        _repository = courseRepository;
        _mapper = mapper;
    }

    public async Task<List<MovieDto>> GetAll()
    {
        var movies = await _repository.MovieRepository.GetAll(false);
        List<MovieDto> moviesDto = _mapper.Map<List<MovieDto>>(movies);
        return moviesDto;
    }

    public async Task<List<MovieDto>> GetWithQuery(MovieRatingParameters movieRatingParams)
    {
        var movies = await _repository.MovieRepository.GetWithQuery(movieRatingParams, false);
        List<MovieDto> moviesDto = _mapper.Map<List<MovieDto>>(movies);
        return moviesDto;
    }

    public async Task<List<MovieDto>> GetAllWithImages()
    {
        var movies = await _repository.MovieRepository.GetAllWithImages(false);
        List<MovieDto> moviesDto = _mapper.Map<List<MovieDto>>(movies);
        return moviesDto;
    }
    
    public async Task<MovieDto> GetById(Guid id)
    {
        var movie = await _repository.MovieRepository.GetById(id, false);
        if (movie is null)
            throw new MovieNotFoundException(id);
        return _mapper.Map<MovieDto>(movie);
    }

    public async Task UpdateMovie(Guid id, UpdateMovieDto movieDto)
    {
        var movieEntity = await _repository.MovieRepository.GetById(id, true);
        if(movieEntity is null)
            throw new MovieNotFoundException(id);
        
        _mapper.Map(movieDto, movieEntity);
        
        //have to pass information about new rating 
        //would be nice to find ratingEntity in service layer
        //but as a temporary option only numbers as shorts are passed
        await _repository.MovieRepository.UpdateMovie(movieEntity, 
            movieDto.GenresNames,
            movieDto.CountriesNames,
            movieDto.Rating?.Imdb,
            movieDto.Rating?.Kp,
            movieDto.Rating?.FilmCritics);
        await _repository.SaveAsync();
    }

    public async Task DeleteMovie(Guid id)
    {
        var movie = await _repository.MovieRepository.GetById(id, false);
        if (movie is null)
            throw new MovieNotFoundException(id);
        _repository.MovieRepository.DeleteMovie(movie);
        //определится с каскадным удалением или некаскадным
        await _repository.SaveAsync();
    }

    public async Task<MovieDto> CreateMovie(PostMovieDto movieDto)
    {   
        var movieEntity = _mapper.Map<MovieEntity>(movieDto);
        
        await _repository.MovieRepository.CreateMovie(movieEntity, movieDto.GenresNames, movieDto.CountriesNames);
        await _repository.SaveAsync();
        
        var movieToReturn = _mapper.Map<MovieDto>(movieEntity);
        return movieToReturn;
    }

    public async Task<ImageInfoDto> GetImageById(Guid id)
    {
        var movieEntity = await _repository.MovieRepository.GetById(id, false);
        
        if(movieEntity is null)
            throw new MovieNotFoundException(id);
        if(movieEntity.ImageInfoEntity is null)
            throw new ImageNotFoundException(id);
        var imageInfoEntity = movieEntity.ImageInfoEntity;
        
        var imageInfoDto = _mapper.Map<ImageInfoDto>(imageInfoEntity);
        return imageInfoDto;
    }
}
