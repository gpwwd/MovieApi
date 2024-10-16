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
        var allItems = JsonConvert.DeserializeObject<List<RootMovieObject>>(unformatedDataString);
        
        // implement deserialization
        return new List<MovieEntity>();
    }
}