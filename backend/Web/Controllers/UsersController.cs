using System.Security.Claims;
using Application.Dtos.GetDtos;
using Application.Dtos.UpdateDtos;
using Application.IServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Filters;

namespace Web.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IUsersService _userService;
    public UsersController(IUsersService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<List<UserDto>>> GetAllUsers()
    {
        var userDTOs = await _userService.GetAll();
        return Ok(userDTOs);
    }

    [HttpPut]
    [ValidationFilter]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
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
        Guid userId = new Guid(userIdClaim);
        
        var addedMoviesIds = await _userService.AddToWatchLaterList(userId, movieIds); 
        return Created("users/add-to-watch-list", addedMoviesIds);
    }

    [HttpDelete]
    [Route("watch-list-movie/{movieId:guid}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
    public async Task<ActionResult> RemoveFromWatchList(Guid movieId)
    {
        string userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
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
