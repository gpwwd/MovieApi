using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Application.Dtos;
using Application.Dtos.GetDtos;
using Application.Dtos.PostDtos;
using Application.IManagers;
using Application.IServices;
using AutoMapper;
using Domain.Entities.UsersEntities;
using Domain.Exceptions.AuthenticationExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services;

public sealed class AuthenticationService : IAuthenticationService
{
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    private readonly IRepositoryManager _repository;
    private readonly IJwtProvider _jwtProvider;
    private readonly UserManager<UserEntity> _userManager;
    private UserEntity? _user;

    public AuthenticationService(IJwtProvider jwtProvider, IConfiguration configuration,
        IMapper mapper, IRepositoryManager repository, UserManager<UserEntity> userManager)
    {
        _jwtProvider = jwtProvider;
        _configuration = configuration;
        _mapper = mapper;
        _repository = repository;
        _userManager = userManager;
    }
    public async Task<UserDto> Register(UserForRegistrationDto userDto)
    {
        var userEntity = _mapper.Map<UserEntity>(userDto);
        userEntity.PasswordHash = PasswordHasher.GeneratePasswordHash(userDto.Password);
        
        await _repository.UserRepository.AddAsync(userEntity);
        await _repository.SaveAsync();
        
        await _repository.UserRepository.AddToRolesAsync(userEntity, userDto.Roles);
        await _repository.SaveAsync();
        
        var userToReturn = _mapper.Map<UserDto>(userEntity);
        return userToReturn;
    }
    
    /// <summary>
    /// 
    /// Перенести токен из локального хранилища в куки на фронтенде
    /// /////////////////////////////////////////////////////////////////////////////////
    ///
    /// Создает и возвращает JwtToken как string
    /// 
    /// </summary>
    public async Task<TokenDto> CreateToken(bool populateExp)
    {
        var claims = await GetClaims();
        var refreshToken = GenerateRefreshToken();
        
        _user = await _repository.UserRepository.GetByName(_user?.UserName!, true);
        _user!.RefreshToken = refreshToken;

        if (populateExp)
            _user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
        
        await _repository.SaveAsync();
        
        var accessToken = _jwtProvider.GenerateToken(claims);
        return new TokenDto(accessToken, refreshToken);
    }
    
    public async Task<TokenDto> RefreshToken(TokenDto tokenDto)
    {
        var principal = GetPrincipalFromExpiredToken(tokenDto.AccessToken);
        var user =  await _repository.UserRepository.GetByName(principal.Identity!.Name!, true);
        if (user == null || user.RefreshToken != tokenDto.RefreshToken ||
            user.RefreshTokenExpiryTime <= DateTime.Now)
            throw new RefreshTokenBadRequest();
        _user = user;
        return await CreateToken(populateExp: false);
    }
    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
    private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)),
            ValidateLifetime = true,
            ValidIssuer = _configuration.GetSection("Jwt")["ValidIssuer"],
            ValidAudience = _configuration.GetSection("Jwt")["ValidAudience"],
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, 
            out var securityToken);
        
        var jwtSecurityToken = securityToken as JwtSecurityToken;
        
        if (jwtSecurityToken == null ||
            !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Invalid token");
        }
        return principal;
    }
    
    public async Task<bool> ValidateUser(UserLoginDto userLoginDto)
    {
        _user = await _repository.UserRepository.GetByEmail(userLoginDto.Email);
        
        if(_user is null)
            return false;
        
        var IsPasswordCorrect = PasswordHasher.Verify(PasswordHasher.GeneratePasswordHash(userLoginDto.Password), _user.PasswordHash!);
        
        if(!IsPasswordCorrect)
            return false;
        return true;
    }

    private async Task<List<Claim>> GetClaims()
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, _user!.UserName!),
        };
     
        var roles = await _repository.UserRepository.GetRolesAsync(_user);

        if (roles != null)
            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role.Name!));
        
        claims.Add(new Claim(ClaimTypes.NameIdentifier, _user.Id.ToString()));
        
        return claims;
    }
}