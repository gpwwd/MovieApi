using Application.Dtos.ExternalApiResponses;
using Application.ExternalApiInterfaces;
using Infrastructure.Database;
using Newtonsoft.Json;

namespace Infrastructure.ExternalApi;

public class KinopoiskApiService : IKinopoiskApiService
{
    private readonly Deserializer _deserializer;
    private readonly MovieDataBaseContext _context;
    private readonly ResponseFetcher _responseFetcher;
    private string FilePath { get; set; } = string.Empty;

    public KinopoiskApiService(Deserializer deserializer, ResponseFetcher responseFetcher, MovieDataBaseContext context)
    {
        _deserializer = deserializer;
        _responseFetcher = responseFetcher;
        _context = context;
    }

    public async Task<string> WriteMoviesToFile()
    {
        var data = await _responseFetcher.GetMoviesData();
        
        FilePath = Directory.GetParent(Directory.GetCurrentDirectory())!.FullName +
                   "/Infrastructure/ExternalApi/MovieFile.json";
        if(!File.Exists(FilePath))
            using (File.Create(FilePath)) { }
        File.AppendAllText(FilePath, data);
        
        return data;
    }

    public async Task AddMoviesToDatabase()
    {
        var unformatedDataString = await File.ReadAllTextAsync(FilePath);
        _deserializer.DeserializeMovies(unformatedDataString);
    }
}