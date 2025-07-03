using Domain.Entities.MovieEntities;
using Newtonsoft.Json;

namespace Infrastructure.ExternalApi;

public class RootMovieObject
{
    [JsonProperty("docs")]
    public List<MovieData> Docs { get; set; }
    
    [JsonProperty("total")]
    public int Total { get; set; }
    
    [JsonProperty("limit")]
    public int Limit { get; set; }
    
    [JsonProperty("page")]
    public int Page { get; set; }
    
    [JsonProperty("pages")]
    public int Pages { get; set; }
}

public class MovieData
{
    [JsonProperty("id")]
    public int Id { get; set; }
    
    [JsonProperty("name")]
    public string Name { get; set; }
    
    [JsonProperty("alternativeName")]
    public string AlternativeName { get; set; }
    
    [JsonProperty("type")]
    public string Type { get; set; }
    
    [JsonProperty("description")]
    public string Description { get; set; }
    
    [JsonProperty("shortDescription")]
    public string ShortDescription { get; set; }
    
    [JsonProperty("rating")]
    public RatingData Rating { get; set; }
    
    [JsonProperty("movieLength")]
    public int MovieLength { get; set; }
    
    [JsonProperty("genres")]
    public List<GenreData> Genres { get; set; }
    
    [JsonProperty("countries")]
    public List<CountryData> Countries { get; set; }
    
    [JsonProperty("budget")]
    public BudgetData Budget { get; set; }
    
    [JsonProperty("top250")]
    public int Top250 { get; set; }
    
    [JsonProperty("isSeries")]
    public bool IsSeries { get; set; }
}

public class RatingData
{
    [JsonProperty("kp")]
    public float Kp { get; set; }
    
    [JsonProperty("imdb")]
    public float Imdb { get; set; }
    
    [JsonProperty("filmCritics")]
    public float FilmCritics { get; set; }
    
    [JsonProperty("russianFilmCritics")]
    public float RussianFilmCritics { get; set; }
}

public class GenreData
{
    [JsonProperty("name")]
    public string Name { get; set; }
}

public class CountryData
{
    [JsonProperty("name")]
    public string Name { get; set; }
}

public class BudgetData
{
    [JsonProperty("currency")]
    public string Currency { get; set; }
    
    [JsonProperty("value")]
    public decimal Value { get; set; }
} 