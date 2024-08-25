using MovieApiMvc.Models.Dtos;

namespace MovieApiMvc.Services.Interfaces
{
    public interface IJwtProvider
    {
        public string GenerateToken(UserLoginDto userInfo);

    }
}