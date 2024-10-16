using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;

namespace Infrastructure.ExternalApi;

public class ResponseFetcher
{
    private readonly HttpClient _httpClient;
    
    public ResponseFetcher()
    {
        _httpClient = new HttpClient()
        {
            BaseAddress = new Uri("https://api.kinopoisk.dev/v1.4/")
        };
    }
    
    public async Task<string> GetMoviesData()
    {
        var query = ShapeQueryData();
        
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(query, UriKind.Relative),
            Headers =
            {
                { "accept", "application/json" },
                { "X-API-KEY",  "A8CBQ2E-1DZMT6Y-J065YW2-0Y5Y7TA"},
            },
        };
        
        HttpResponseMessage response = await _httpClient.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            string data = await response.Content.ReadAsStringAsync();
            return data;
        }
        throw new BadHttpRequestException("Bad Request To Kinopoisk Api", (int)response.StatusCode);
    }

    private string ShapeQueryData()
    {
        var selectedFileds = new List<string>()
        {
            "id", "name", "alternativeName", "type", "isSeries", "rating", "budget", "movieLength",
            "genres", "countries", "top250", "description", "shortDescription",
        };

        var notNullFields = new List<string>()
        {
            "top250"
        };
        var query = CreateStringQuery("1", "2", selectedFileds, notNullFields,
            "top250", "1", "1", "7.2-10");
        
        return query;
    }
    
    private string CreateStringQuery(string page, string perPage, List<string> selectFields, List<string> notNullFields,
        string sortField, string sortType, string typeNumber, string ratingKp)
    {
        var queryString = new Dictionary<string, List<string>>();
        
        AddQueryParameter(queryString, "page", page);
        AddQueryParameter(queryString, "limit", perPage);
        AddQueryParameters(queryString, "selectFields", selectFields);
        AddQueryParameters(queryString, "notNullFields", notNullFields);
        AddQueryParameter(queryString, "sortField", sortField);
        AddQueryParameter(queryString, "sortType", sortType);
        AddQueryParameter(queryString, "typeNumber", typeNumber);
        AddQueryParameter(queryString, "rating.kp", ratingKp);

        var requestUri = "movie";

        foreach (var kvp in queryString)
        {
            foreach (var value in kvp.Value)
            {
                requestUri = QueryHelpers.AddQueryString(requestUri, kvp.Key, value);
            }
        }

        return requestUri;
    }
    
    private void AddQueryParameter(Dictionary<string, List<string>> queryString, string key, string value)
    {
        if (!queryString.ContainsKey(key))
        {
            queryString[key] = new List<string>();
        }
        queryString[key].Add(value);
    }
    
    private void AddQueryParameters(Dictionary<string, List<string>> queryString, string key, List<string> values)
    {
        if (!queryString.ContainsKey(key))
        {
            queryString[key] = new List<string>();
        }
        queryString[key].AddRange(values);
    }
}