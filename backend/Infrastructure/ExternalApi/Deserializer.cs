using Application.Dtos.ExternalApiResponses;
using Domain.Entities.MovieEntities;
using Newtonsoft.Json;
using RootObject = Application.Dtos.ExternalApiResponses.RootObject;

namespace Infrastructure.ExternalApi;

public class Deserializer
{
    public List<ImageInfo> GetMoviesUrlsCovers()
    {
        var json = File.ReadAllText("ExternalApi/posters.json");
        var allItems = JsonConvert.DeserializeObject<List<RootObject>>(json);

        var result = new List<ImageInfo>();
            
        int counter = 0;
        foreach (var item in allItems)
        {
            var imageInfo = new ImageInfo();
            imageInfo.PreviewUrls = new List<string>();
            imageInfo.Urls = new List<string>();

            try
            {
                counter++;
                imageInfo.MovieId = item.Docs[0].MovieId;
            }
            catch(IndexOutOfRangeException ex)
            {
                Console.WriteLine(counter);
            }

        
            foreach (var doc in item.Docs)
            {
                imageInfo.Urls.Add(doc.Url);
                imageInfo.PreviewUrls.Add(doc.PreviewUrl);
            }
            result.Add(imageInfo);
        }
            
        return result;
    }

    public List<MovieEntity> DeserializeMovies(string unformatedDataString)
    {
        var root = JsonConvert.DeserializeObject<RootMovieObject>(unformatedDataString);
        if (root?.Docs == null) return new List<MovieEntity>();

        return root.Docs.Select(item => new MovieEntity
        {
            Id = Guid.NewGuid(),
            Name = item.Name,
            AlternativeName = item.AlternativeName,
            Type = item.Type,
            Description = item.Description,
            ShortDescription = item.ShortDescription,
            MovieLength = item.MovieLength,
            Top250 = item.Top250,
            IsSeries = item.IsSeries,
            Rating = new RatingEntity
            {
                Id = Guid.NewGuid(),
                Kp = (short)item.Rating.Kp,
                Imdb = (short)item.Rating.Imdb,
                FilmCritics = (short)item.Rating.FilmCritics,
                RussianFilmCritics = (short)item.Rating.RussianFilmCritics
            },
            Budget = item.Budget != null ? new BudgetEntity
            {
                Id = Guid.NewGuid(),
                Currency = item.Budget.Currency,
                Value = (double)item.Budget.Value
            } : null,
            Genres = item.Genres?.Select(g => new GenreEntity 
            { 
                Id = Guid.NewGuid(),
                Name = g.Name 
            }).ToList() ?? new List<GenreEntity>(),
            Countries = item.Countries?.Select(c => new CountryEntity 
            { 
                Id = Guid.NewGuid(),
                Name = c.Name 
            }).ToList() ?? new List<CountryEntity>()
        }).ToList();
    }
}