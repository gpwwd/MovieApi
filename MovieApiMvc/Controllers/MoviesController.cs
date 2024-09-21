using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApiMvc.Services.Interfaces;
using MovieApiMvc.Filters;
using MovieApiMvc.Models.Dtos;
using MovieApiMvc.Models.Dtos.GetDtos;
using MovieApiMvc.RequestFeatures;

namespace MovieApiMvc.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MoviesController : Controller
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
    // it else can be done like
    // [Produces(typeof(Employee))]
    // public IActionResult Get(long id)
    // {
    //     var employee = GetEmployee(id);
    //     return Ok(employee);
    // }

    [HttpPut]
    //  чо это [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<ActionResult> PutMovie([FromHeader] Guid id, [FromBody] MovieDto movie)
    {       
        await _moviesService.PutMovie(id, movie);
        return Ok(id);
    } 
    
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteMovie(Guid id)
    {
        try
        {
            await _moviesService.DeleteMovie(id);
            return new NoContentResult();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            Console.WriteLine(ex.Message);
            return StatusCode(500);
        }
    }

    [HttpPost]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<ActionResult> CreateMovie(MovieDto movie)
    {
        var createdEntity = await _moviesService.CreateMovie(movie);   
        return CreatedAtRoute("CompanyById", new { id = createdEntity.Id }, createdEntity);
    }

}