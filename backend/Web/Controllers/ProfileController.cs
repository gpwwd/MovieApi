using System.Security.Claims;
using Application.Dtos.UpdateDtos;
using Application.IManagers;
using Application.IServices;
using Domain.Entities.UsersEntities;
using Domain.Exceptions.NotFoundExceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web.Filters;

namespace Web.Controllers;

[Route("api/profile")]
[ApiController]
public class ProfileController : ControllerBase
{
    private readonly IUsersService _userService;
    private readonly IRepositoryManager _repositoryManager;
    private readonly UserManager<UserEntity> _userManager;
    
    public ProfileController(IUsersService userService, UserManager<UserEntity> userManager
        , IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
        _userService = userService;
        _userManager = userManager;
    }
    
    [HttpPatch]
    [Authorize]
    [Route("update-username")]
    [ValidationFilter]
    public async Task<ActionResult> UpdateUsername([FromBody] UpdateNameDto nameDto)
    {
        var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?
            .Value.ToString();
        if(User.Identity == null)
            throw new UnauthorizedAccessException();
        
        var user = await _userService.GetByNameAsync(User.Identity.Name!);
        if (user is null)
            throw new UserNotFoundException(new Guid(userIdClaim!));
        
        user.UserName = nameDto.Name;
        await _repositoryManager.SaveAsync();

        return Ok(user);
    } 
}