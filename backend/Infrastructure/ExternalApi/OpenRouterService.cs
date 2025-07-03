using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Domain.Entities.MovieEntities;

namespace Infrastructure.ExternalApi;

public class OpenRouterService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    private const string BaseUrl = "https://openrouter.ai/api/v1";

    public OpenRouterService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _apiKey = Environment.GetEnvironmentVariable("OPENROUTER_API_KEY") ?? 
            throw new InvalidOperationException("OPENROUTER_API_KEY is not configured");
            
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
        _httpClient.DefaultRequestHeaders.Add("HTTP-Referer", "https://localhost");
        _httpClient.DefaultRequestHeaders.Add("X-Title", "MovieApi");
    }

    public async Task<string> GetMovieRecommendations(List<MovieEntity> favoriteMovies)
    {
        var moviesContext = favoriteMovies.Select(m => new
        {
            m.Name,
            m.Description,
            Genres = string.Join(", ", m.Genres.Select(g => g.Name)),
            m.Year
        }).ToList();

        var prompt = $@"Based on the user's favorite movies:

    {JsonConvert.SerializeObject(moviesContext, Formatting.Indented)}

    Recommend 5 similar movies they might enjoy. Consider genres, themes, and plot elements.
    Format your response as a JSON array with movie titles and brief explanations why they might like each one.
    Keep explanations concise (max 2 sentences).";

        return await SendRequest(prompt);
    }

    public async Task<string> AnalyzeUserPreferences(List<MovieEntity> favoriteMovies)
    {
        var moviesContext = favoriteMovies.Select(m => new
        {
            m.Name,
            m.Description,
            Genres = string.Join(", ", m.Genres.Select(g => g.Name)),
            m.Year
        }).ToList();

        var prompt = $@"Analyze the user's movie preferences based on their favorite movies:

    {JsonConvert.SerializeObject(moviesContext, Formatting.Indented)}

    Provide a brief analysis of:
    1. Preferred genres
    2. Themes they enjoy
    3. Time periods they prefer
    4. Any other notable patterns

    Format response as a concise JSON object with these categories.";

        return await SendRequest(prompt);
    }

    private async Task<string> SendRequest(string prompt)
    {
        var request = new
        {
            model = "mistralai/mistral-7b-instruct",
            messages = new[]
            {
                new { role = "system", content = "You are a helpful movie recommendation assistant. Always respond in valid JSON format." },
                new { role = "user", content = prompt }
            },
            temperature = 0.7,
            max_tokens = 1000,
            response_format = new { type = "json_object" }
        };

        var response = await _httpClient.PostAsync($"{BaseUrl}/chat/completions",
            new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));

        var result = await response.Content.ReadAsStringAsync();
        
        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine($"API Error: Status {response.StatusCode}");
            Console.WriteLine($"Response content: {result}");
            throw new InvalidOperationException($"API request failed with status {response.StatusCode}: {result}");
        }

        try
        {
            var jsonResponse = JObject.Parse(result);
            var messageContent = jsonResponse["choices"]?[0]?["message"]?["content"]?.ToString();
            
            if (string.IsNullOrEmpty(messageContent))
            {
                throw new InvalidOperationException("Empty response from OpenRouter API");
            }

            JToken.Parse(messageContent);
            return messageContent;
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"JSON parsing error: {ex.Message}");
            Console.WriteLine($"Response content: {result}");
            throw;
        }
    }
} 