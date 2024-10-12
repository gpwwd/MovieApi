using Application.Dtos;
using Application.Dtos.PostDtos;
using Application.IServices;
using Microsoft.AspNetCore.Mvc;
using Web.Filters;

namespace Web.Controllers;

[Route("api/authentication")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }
    
    /// <summary>
    /// Use "UserManager" class later
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    [HttpPost("register")]
    [ValidationFilter]
    public async Task<ActionResult> Register([FromBody] UserForRegistrationDto user)
    {
        var createdEntity = await _authenticationService.Register(user);
        return Created("users/register", createdEntity);
    }
    
    [HttpPost("login")]
    [ValidationFilter]
    public async Task<ActionResult> Login([FromBody] UserLoginDto userLoginDto)
    {
        if (!await _authenticationService.ValidateUser(userLoginDto))
            return Unauthorized();
        
        var token = await _authenticationService.CreateToken(populateExp: true);
        return Ok( token );
    }
    
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody]TokenDto tokenDto)
    {
        var tokenDtoToReturn = await
            _authenticationService.RefreshToken(tokenDto);
        return Ok(tokenDtoToReturn);
    }
}