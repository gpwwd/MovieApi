using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
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
        try
        {
            var token = await _userService.Login(userLoginDto);
            if (token is null)
            {
                return Unauthorized();
            }
            return Ok(new {token = token});
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

    [HttpPost]
    [Route("addToWatchList")]
    [Authorize]
    public async Task<ActionResult<List<MovieDto>>> AddToWatchLaterList([FromBody] Guid[] movieIds)
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
        var location = Url.Action("addToWatchList");
        return Created(location, addedMoviesIds);
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
