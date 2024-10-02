using System.Security.Claims;

namespace MovieApiMvc.Services.Interfaces
{
    public interface IJwtProvider
    {
        public string GenerateToken(List<Claim> claims);

    }
}