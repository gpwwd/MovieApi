using Application.Dtos.GetDtos;
using Application.Dtos.PostDtos;
using Application.Dtos.UpdateDtos;
using Application.IServices;
using Application.RequestFeatures;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

    [HttpGet("get-with-images")]
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
    public async Task<ActionResult> CreateMovie([FromBody] PostMovieDto movie)
    {
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        // movie.Budget.Currency = $"{movie.Budget.Currency[0].ToString().ToUpper()}{movie.Budget.Currency.Substring(1)}";
        //
        // ModelState.ClearValidationState(nameof(PostMovieDto));
        // if (!TryValidateModel(movie, nameof(PostMovieDto)))
        //     return UnprocessableEntity(ModelState);
        
        var createdMovieDto = await _moviesService.CreateMovie(movie);   
        return Created("CreateMovie", createdMovieDto);
    }

}