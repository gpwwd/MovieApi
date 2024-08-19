using Microsoft.AspNetCore.Mvc;
using MovieApiMvc.DataBaseAccess.Repositories;
using MovieApiMvc.DataBaseAccess.Entities;
using MovieApiMvc.Dtos;
using Microsoft.EntityFrameworkCore;
using MovieApiMvc.Services.Interfaces;

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

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<MovieDto>> GetMovie(Guid id)
    {
        //HttpResponse response = HttpContext.Response;
        
        var movieDTO = await _moviesService.GetById(id);
        if(movieDTO is null)
        {
            return new NotFoundResult();
        }
        return movieDTO;
    }
    // it alse can be done like
    // [Produces(typeof(Employee))]
    // public IActionResult Get(long id)
    // {
    //     var employee = GetEmployee(id);
    //     return Ok(employee);
    // }

    [HttpPut]
    public async Task<ActionResult> PutMovie([FromHeader] Guid id, [FromBody] MovieDto movie)
    {       
        if (movie is null)
        {
            return new BadRequestResult();
        }
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
    public async Task<ActionResult> CreateMovie(MovieDto movie)
    {
        if (movie is null)
        {
            return BadRequest();
        }
        
        var createdEntity = await _moviesService.CreateMovie(movie);
        var location = Url.Action("Post", new { id = createdEntity.Id });
        return Created(location, createdEntity);
    }

}