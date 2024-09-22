using AutoMapper;
using MovieApiMvc.Services.Interfaces;
using MovieApiMvc.DataBaseAccess.Entities.MovieEntities;
using MovieApiMvc.ErrorHandling;
using MovieApiMvc.Models.Dtos.GetDtos;
using MovieApiMvc.Models.Dtos.PostDtos;
using MovieApiMvc.RequestFeatures;

namespace MovieApiMvc.Services;

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

    public async Task<List<MovieDto>> GetWithPaging(MovieParameters movieParams)
    {
        var movies = await _repository.MovieRepository.GetWithPaging(movieParams, false);
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

    public async Task PutMovie(Guid id, MovieDto movieDto)
    {
        var movieEntity = await _repository.MovieRepository.GetById(id, true);
        if(movieEntity is null)
                throw new MovieNotFoundException(id);
        _mapper.Map(movieDto, movieEntity);
        Console.WriteLine(movieEntity.Genres[0].Name);
        //call saving repo method
        //await _moviesRepository.Update(movieEntity);
    }

    public async Task DeleteMovie(Guid id)
    {
        //await _repository.Delete(id);
    }

    public async Task<MovieDto> CreateMovie(PostMovieDto movieDto, IEnumerable<Guid> genresId, 
        IEnumerable<Guid> countriesId, Guid ratingId)
    {   
        var movieEntity = _mapper.Map<MovieEntity>(movieDto);
        
        _repository.MovieRepository.CreateMovie(movieEntity);
        _repository.Save();
        
        var movieToReturn = _mapper.Map<MovieDto>(movieEntity);
        return movieToReturn;
    }

    public async Task<ImageInfoDto> GetImageById(Guid id)
    {
        // var movie = await _repository.GetById(id);
        //
        // if(movie is null )
        // {
        //     throw new MovieNotFoundException(id);
        // }
        // if(movie.ImageInfoEntity is null)
        // {
        //     throw new ImageNotFoundException(id);
        // }
        //
        // var imageInfoEntity = movie.ImageInfoEntity;

        return new ImageInfoDto
        { };
    }//move to another service and controller later
}
