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

/// <summary>
/// В качестве пользователя всегда использовать свойство
/// User контроллера, чтобы получить доступ только к залогинненного пользователю,
/// так как изменять свой профиль может только залогинненый пользователь.
/// Осуществлять поиск пользователя в базе данных с помощью передачи в методы UserManager
/// полей из DTO нельзя, так как в таком случае возможно изменение профиля не залогинненного пользователя
/// </summary>
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
    
    /// <summary>
    /// UserManager ведет поиск по normalizedUserName, поэтому для его использования
    /// нужно создавать пользователей также через UserManager.
    /// В этом случае воспользуемся кастомным UserService 
    /// </summary>
    /// <param name="nameDto"></param>
    /// <returns></returns>
    /// <exception cref="UnauthorizedAccessException"></exception>
    /// <exception cref="UserNotFoundException"></exception>
    [HttpPatch]
    [Authorize]
    [Route("update-username")]
    [ServiceFilter(typeof(ValidationFilter))]
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
    
    ////////////////////////////////////////////////////////////////
    // add other patch methods 
}