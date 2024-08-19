using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApiMvc.Dtos;
using MovieApiMvc.ErrorHandling;
using MovieApiMvc.Models.Dtos;
using MovieApiMvc.Services.Interfaces;

namespace MovieApiMvc.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : Controller
{
    private readonly IUsersService _userService;
    public UsersController(IUsersService userService)
    {
        _userService = userService;
    }
    
    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] UserDto user)
    {
        if (user is null)
        {
            return BadRequest();
        }
        
        var createdEntity = await _userService.Register(user);
        var location = Url.Action("Post", new { id = createdEntity.Id });
        return Created(location, createdEntity);
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] UserLoginDto userLoginDto)
    {   
        var token = await _userService.Login(userLoginDto);

        if (token is null)
        {
            return Unauthorized();
        }
        
        return Ok(token);
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<List<UserDto>>> GetAllUsers()
    {
        var userDTOs = await _userService.GetAll();
        return Ok(userDTOs);
    }

    [HttpPost]
    public async Task<ActionResult> CreateUser([FromBody] UserDto user)
    {
        if (user is null)
        {
            return BadRequest();
        }
        
        var createdEntity = await _userService.CreateUser(user);
        var location = Url.Action("Post", new { id = createdEntity.Id });
        return Created(location, createdEntity);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateMovie([FromBody] UserDto user)
    {       
        if (user is null)
        {
            return new BadRequestResult();
        }
        await _userService.UpdateUser(user);
        return Ok(user);
    } 

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<UserDto>> GetUser(Guid id)
    {

        var user = await _userService.GetById(id);
        if(user is null)
        {
            return new NotFoundResult();
        }
        return user;
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteUser(Guid id)
    {
        try
        {
            await _userService.DeleteUser(id);
            return new NoContentResult();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            Console.WriteLine(ex.Message);
            return StatusCode(500);
        }
    }

    [HttpPost]
    [Route("{userId}/addToWatchList")]
    public async Task<ActionResult<List<MovieDto>>> AddToWatchLaterList(Guid userId, [FromBody] Guid[] movieIds)
    {
        List<Guid> addedMoviesIds = new List<Guid>();
        try
        {
            addedMoviesIds = await _userService.AddToWatchLaterList(userId, movieIds); 
        }
        catch(EntityNotFoundException ex)
        {   
            Console.WriteLine(ex.Message);
            return NotFound();
        }
        catch(EntityAlreadyExistException ex)
        {
            return Conflict(new { message = ex.Message});
        } 
        var location = Url.Action("{userId}/course");
        return Created(location, addedMoviesIds);
    }
    
    [HttpGet]
    [Route("{userId}/watchList")]
    [Authorize]
    public async Task<ActionResult<List<MovieDto>>> GetWatchLaterList(Guid userId)
    {   
        try
        {
            var watchLaterMovies = await _userService.GetWatchLaterMovies(userId);
            return Ok(watchLaterMovies);
        }
        catch(EntityNotFoundException ex)
        {
            Console.WriteLine(ex.Message);
            return NotFound();
        }
    }
}
