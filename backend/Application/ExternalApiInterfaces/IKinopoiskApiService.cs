namespace Application.ExternalApiInterfaces;

public interface IKinopoiskApiService
{
    public Task<string> WriteMoviesToFile();
    public Task AddMoviesToDatabase();
}