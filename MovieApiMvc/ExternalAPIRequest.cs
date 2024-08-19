using System.Net.Http.Headers;
using Microsoft.VisualBasic;

namespace ExternalAPIServiceSpace;

public static class ExternalAPIService{
    public static async Task<string> GetData(){
        var client = new HttpClient();
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri("https://api.kinopoisk.dev/v1.4/image?page=1&limit=10&movieId=1108577"),
            //https://api.kinopoisk.dev/v1.4/movie?page=1&limit=250&selectFields=id&selectFields=name&selectFields=alternativeName&selectFields=type&selectFields=isSeries&selectFields=year&selectFields=rating&selectFields=budget&selectFields=movieLength&selectFields=genres&selectFields=countries&selectFields=top250&notNullFields=top250&sortField=top250&sortType=1&typeNumber=1&rating.kp=7.2-10
            Headers =
            {
                { "accept", "application/json" },
                { "X-API-KEY",  "your key"},
            },
        };
        using (var response = await client.SendAsync(request))
        {
            response.EnsureSuccessStatusCode();
            var resp = await response.Content.ReadAsStringAsync();
            return resp;
        }
    }
}   