using Application.Dtos;
using Application.Dtos.GetDtos;
using Application.Dtos.PostDtos;

namespace Application.IServices;
public interface IAuthenticationService
{
    public Task<UserDto> Register(UserForRegistrationDto userDto);
    public Task<bool> ValidateUser(UserLoginDto userLoginDto);
    public Task<TokenDto> CreateToken(bool populateExp);
    Task<TokenDto> RefreshToken(TokenDto tokenDto);
}