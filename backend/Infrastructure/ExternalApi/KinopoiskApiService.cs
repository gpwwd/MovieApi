using Application.Dtos.ExternalApiResponses;
using Application.ExternalApiInterfaces;
using Infrastructure.Database;
using Newtonsoft.Json;
using Domain.Entities.MovieEntities;

namespace Infrastructure.ExternalApi;

public class KinopoiskApiService : IKinopoiskApiService
{
    private readonly Deserializer _deserializer;
    private readonly MovieDataBaseContext _context;
    private readonly ResponseFetcher _responseFetcher;
    private string FilePath { get; set; } = Directory.GetParent(Directory.GetCurrentDirectory())!.FullName +
                   "/Infrastructure/ExternalApi/MovieFile.json";

    public KinopoiskApiService(Deserializer deserializer, ResponseFetcher responseFetcher, MovieDataBaseContext context)
    {
        _deserializer = deserializer;
        _responseFetcher = responseFetcher;
        _context = context;
    }

    public async Task<string> WriteMoviesToFile()
    {
        var data = await _responseFetcher.GetMoviesData();
        
        if(!File.Exists(FilePath))
            using (File.Create(FilePath)) { }
        File.AppendAllText(FilePath, data);
        
        return data;
    }

    public async Task AddMoviesToDatabase()
    {
        var unformatedDataString = await File.ReadAllTextAsync(FilePath);
        List<MovieEntity> movies = _deserializer.DeserializeMovies(unformatedDataString);
        foreach (var movie in movies)
        {
            Console.WriteLine($"Id: {movie.Id}, Name: {movie.Name}, AlternativeName: {movie.AlternativeName}, Type: {movie.Type}, Description: {movie.Description}, ShortDescription: {movie.ShortDescription}, MovieLength: {movie.MovieLength}, Top250: {movie.Top250}, IsSeries: {movie.IsSeries}, Rating: {movie.Rating.Kp}, Budget: {movie.Budget?.Currency} {movie.Budget?.Value}, Genres: {string.Join(", ", movie.Genres.Select(g => g.Name))}, Countries: {string.Join(", ", movie.Countries.Select(c => c.Name))}");
        }
    }
}