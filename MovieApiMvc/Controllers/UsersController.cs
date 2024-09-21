using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        try
        {
            var token = await _userService.Login(userLoginDto);
            if (token is null)
            {
                return Unauthorized();
            }
            return Ok( token );
        }
        catch(MyExeption ex)
        {
            return Unauthorized(ex.Message);
        }

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

    // [HttpGet]
    // [Route("testAuth")]
    // [Authorize]
    // public async Task<IActionResult> testAuth()
    // {
    //     User.AddIdentity
    //     return Ok();
    // }

    [HttpPost]
    [Route("addToWatchList")]
    [Authorize]
    public async Task<ActionResult<List<MovieDto>>> AddToWatchLaterList([FromBody] string[] movieIds)
    {
        string userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;//!- переменная не будет null, если null - runtime exception

        Guid userId;
        Guid[] movieGuids = new Guid[movieIds.Length]; 
        try
        {
            userId = new Guid(userIdClaim);
            for (int i = 0; i < movieIds.Length; i++)
            {
                movieGuids[i] = new Guid(movieIds[i]); 
            }
        }
        catch
        {
            userId = Guid.Empty;
        }

        List<Guid> addedMoviesIds = new List<Guid>();
        try
        {
            addedMoviesIds = await _userService.AddToWatchLaterList(userId, movieGuids); 
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
        var location = Url.Action("addToWatchList");
        return Created(location, addedMoviesIds);
    }

    [HttpDelete]
    [Route("{movieId:guid}/removeFromWatchList")]
    [Authorize]
    public async Task<ActionResult> RemoveFromWatchList(string movieId)
    {
        string userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;//!- переменная не будет null, если null - runtime exception

        Guid movieIdGuid;
        Guid userId;
        try
        {
            movieIdGuid = new Guid(movieId);
            userId = new Guid(userIdClaim);
        }
        catch
        {
            movieIdGuid = Guid.Empty;
            userId = Guid.Empty;
        }

        try
        {
            await _userService.RemoveWatchLaterUser(userId, movieIdGuid);
            return new NoContentResult();
        }
        catch (EntityNotFoundException ex)
        {
            Console.WriteLine(ex.Message);
            return NotFound(ex.Message);
        }
    }
    
    [HttpGet]
    [Route("watchList")]
    [Authorize]
    public async Task<ActionResult<List<MovieDto>>> GetWatchLaterList()
    {   
        string userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;//!- переменная не будет null, если null - runtime exception

        Guid userId;
        try
        {
            userId = new Guid(userIdClaim);
        }
        catch
        {
            userId = Guid.Empty;
        }

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

    [HttpGet]
    [Authorize]
    [Route("GetTestAuth")]
    public async Task<ActionResult> GetTestAuth()
    {

        var emailClaim = User.FindFirst(ClaimTypes.Email)?.Value;
        string curUserEmail = String.Empty;
        return Ok();
    }

}
