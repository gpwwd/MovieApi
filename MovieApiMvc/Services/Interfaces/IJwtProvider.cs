namespace MovieApiMvc.Services.Interfaces
{
    public interface IJwtProvider
    {
        public string GenerateToken();//can use User params later as a params for generating token (Claims)

    }
}