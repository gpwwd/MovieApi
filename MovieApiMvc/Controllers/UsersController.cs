using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieApiMvc.Models.Dtos;
using MovieApiMvc.Models.Dtos.GetDtos;
using MovieApiMvc.Models.Dtos.PostDtos;
using MovieApiMvc.Models.Dtos.UpdateDtos;
using MovieApiMvc.Services.Interfaces;

namespace MovieApiMvc.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUsersService _userService;
    public UsersController(IUsersService userService)
    {
        _userService = userService;
    }
    
    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] UserForRegistrationDto user)
    {
        var createdEntity = await _userService.Register(user);
        return Created("users/register", createdEntity);
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] UserLoginDto userLoginDto)
    {   
        var token = await _userService.Login(userLoginDto);
        return Ok( token );
    }

    [HttpGet]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<List<UserDto>>> GetAllUsers()
    {
        var userDTOs = await _userService.GetAll();
        return Ok(userDTOs);
    }

    [HttpPut]
    [Authorize]
    public async Task<ActionResult> UpdateUser([FromHeader] Guid id, [FromBody] UserUpdateDto user)
    {       
        await _userService.UpdateUser(id, user);
        return Ok(user);
    } 

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<UserDto>> GetUser(Guid id)
    {
        var user = await _userService.GetById(id);
        return user;
    }

    [HttpDelete("{id:guid}")]
    [Authorize]
    public async Task<ActionResult> DeleteUser(Guid id)
    {
        await _userService.DeleteUser(id);
        return new NoContentResult();
    }

    [HttpPost]
    [Route("watch-list-movie")]
    [Authorize]
    public async Task<ActionResult<List<MovieDto>>> AddToWatchLaterList([FromBody] Guid[] movieIds)
    {
        string userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        //проверить userIdClaim если null
        Guid userId = new Guid(userIdClaim);
        
        var addedMoviesIds = await _userService.AddToWatchLaterList(userId, movieIds); 
        return Created("users/add-to-watch-list", addedMoviesIds);
    }

    [HttpDelete]
    [Route("watch-list-movie/{movieId:guid}")]
    [Authorize]
    public async Task<ActionResult> RemoveFromWatchList(Guid movieId)
    {
        string userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        // проверить userIdClaim если null
        Guid userId = new Guid(userIdClaim);
        
        await _userService.RemoveWatchLaterUser(userId, movieId);
        return new NoContentResult();
    }
    
    [HttpGet]
    [Route("watch-list-movies")]
    [Authorize]
    public async Task<ActionResult<List<MovieDto>>> GetWatchLaterList()
    {   
        string userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        Guid userId = new Guid(userIdClaim);
        
        var watchLaterMovies = await _userService.GetWatchLaterMovies(userId);
        return Ok(watchLaterMovies);
    }

}
