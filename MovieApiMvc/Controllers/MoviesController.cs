using Microsoft.AspNetCore.Mvc;
using MovieApiMvc.Services.Interfaces;
using MovieApiMvc.Filters;
using MovieApiMvc.Models.Dtos.GetDtos;
using MovieApiMvc.Models.Dtos.PostDtos;
using MovieApiMvc.Models.Dtos.UpdateDtos;
using MovieApiMvc.RequestFeatures;

namespace MovieApiMvc.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MoviesController : ControllerBase
{
    private readonly IMoviesService _moviesService;
    public MoviesController(IMoviesService moviesService)
    {
        _moviesService = moviesService;
    }

    [HttpGet]
    public async Task<ActionResult<List<MovieDto>>> GetAllMovies()
    {
        var movieDTOs = await _moviesService.GetAll();
        return Ok(movieDTOs);
    }

    [HttpGet]
    [Route("page")]
    public async Task<ActionResult<List<MovieDto>>> GetMoviesPaging([FromQuery] MovieParameters movieParameters)
    {
        var movieDTOs = await _moviesService.GetWithPaging(movieParameters);
        return Ok(movieDTOs);
    }

    [HttpGet("images")]
    public async Task<ActionResult<List<MovieDto>>> GetAllMoviesWithImages()
    {
        var movieDTOs = await _moviesService.GetAllWithImages();
        return Ok(movieDTOs);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<MovieDto>> GetMovie(Guid id)
    {
        var movieDTO = await _moviesService.GetById(id);
        return movieDTO;
    }

    [HttpGet("{id:guid}/images")]
    public async Task<ActionResult<ImageInfoDto>> GetMoviePoster(Guid id)
    {
        var imageInfoDto = await _moviesService.GetImageById(id);
        return imageInfoDto;
    }

    [HttpPut]
    public async Task<ActionResult> UpdateMovie([FromHeader] Guid id, [FromBody] UpdateMovieDto movie)
    {       
        await _moviesService.UpdateMovie(id, movie);
        return Ok(id);
    } 
    
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteMovie(Guid id)
    {
        await _moviesService.DeleteMovie(id);
        return new NoContentResult();
    }

    [HttpPost]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<ActionResult> CreateMovie([FromBody] PostMovieDto movie)
    {
        var createdMovieDto = await _moviesService.CreateMovie(movie);   
        return CreatedAtRoute("CreateMovie", new { id = createdMovieDto.Id }, createdMovieDto);
    }

}