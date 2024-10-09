using System.Security.Claims;

namespace Application.IServices
{
    public interface IJwtProvider
    {
        public string GenerateToken(List<Claim> claims);
    }
}