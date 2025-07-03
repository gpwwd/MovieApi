using Application.Dtos.GetDtos;
using Application.Dtos.PostDtos;
using Application.Dtos.UpdateDtos;
using Application.IServices;
using Application.RequestFeatures;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Filters;

namespace Web.Controllers;

[Route("api/movies")]
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
        var movieDtos = await _moviesService.GetAll();
        return Ok(movieDtos);
    }

    [HttpGet]
    [Route("page")]
    [TypeFilter(typeof(QueryValidationFilter))]
    public async Task<ActionResult<List<MovieDto>>> GetMoviesWithQuery([FromQuery] MovieRatingParameters movieRatingParameters)
    {
        var movieDtos = await _moviesService.GetWithQuery(movieRatingParameters);
        return Ok(movieDtos);
    }

    [HttpGet("get-with-images")]
    public async Task<ActionResult<List<MovieDto>>> GetAllMoviesWithImages()
    {
        var movieDtos = await _moviesService.GetAllWithImages();
        return Ok(movieDtos);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<MovieDto>> GetMovie(Guid id)
    {
        var movieDto = await _moviesService.GetById(id);
        return movieDto;
    }

    [HttpGet("{id:guid}/images")]
    public async Task<ActionResult<ImageInfoDto>> GetMoviePoster(Guid id)
    {
        var imageInfoDto = await _moviesService.GetImageById(id);
        return imageInfoDto;
    }

    [HttpPut]
    [ValidationFilter]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
    public async Task<ActionResult> UpdateMovie([FromHeader] Guid id, [FromBody] UpdateMovieDto movie)
    {      
        await _moviesService.UpdateMovie(id, movie);
        return Ok(id);
    } 
    
    [HttpDelete("{id:guid}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
    public async Task<ActionResult> DeleteMovie(Guid id)
    {
        await _moviesService.DeleteMovie(id);
        return new NoContentResult();
    }

    [HttpPost]
    [ValidationFilter]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
    public async Task<ActionResult> CreateMovie([FromBody] PostMovieDto movie)
    {
        var createdMovieDto = await _moviesService.CreateMovie(movie);   
        return Created("CreateMovie", createdMovieDto);
    }
}