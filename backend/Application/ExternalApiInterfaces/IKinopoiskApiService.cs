namespace Application.ExternalApiInterfaces;

public interface IKinopoiskApiService
{
    public Task<string> GetMoviesData();
}