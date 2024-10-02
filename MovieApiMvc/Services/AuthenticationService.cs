using System.Security.Claims;
using AutoMapper;
using MovieApiMvc.DataBaseAccess.Entities.UsersEntities;
using MovieApiMvc.ErrorHandling.AuthenticationExtensions;
using MovieApiMvc.Models.Dtos;
using MovieApiMvc.Models.Dtos.GetDtos;
using MovieApiMvc.Models.Dtos.PostDtos;
using MovieApiMvc.Services.Interfaces;

namespace MovieApiMvc.Services;

public sealed class AuthenticationService : IAuthenticationService
{
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    private readonly IRepositoryManager _repository;
    private readonly IJwtProvider _jwtProvider;
    private UserEntity? _user;    
    public AuthenticationService(IJwtProvider jwtProvider, IConfiguration configuration,
        IMapper mapper, IRepositoryManager repository)
    {
        _jwtProvider = jwtProvider;
        _configuration = configuration;
        _mapper = mapper;
        _repository = repository;
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
    /// move token from local storage to cookies on frontend
    /// /////////////////////////////////////////////////////////////////////////////////
    /// If the identifier and secret are valid, the app can set the principal for the
    /// current request, but it also needs a way of storing these details for
    /// subsequent requests. For traditional web apps, this is typically achieved
    /// by storing an encrypted version of the user principal in a cookie.
    /// </summary>
    public async Task<string> Login(UserLoginDto userDto)
    {
        _user = await _repository.UserRepository.GetByEmail(userDto.Email);
        
        if(_user is null)
            throw new NotRegistredException(userDto.Email);
        
        var IsPasswordCorrect = PasswordHasher.Verify(PasswordHasher.GeneratePasswordHash(userDto.Password), _user.PasswordHash!);
        
        if(!IsPasswordCorrect)
            throw new IncorrectPasswordException(userDto.Password);

        var claims = await GetClaims();
        var token = _jwtProvider.GenerateToken(claims);
        return token;
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