// namespace Web.ExternalApi;
//
// public static class ExternalAPIService{
//     public static async Task<string> GetData(){
//         var client = new HttpClient();
//         var request = new HttpRequestMessage
//         {
//             Method = HttpMethod.Get,
//             RequestUri = new Uri("https://api.kinopoisk.dev/v1.4/movie?page=1&limit=250&selectFields=id&selectFields=name&selectFields=alternativeName&selectFields=type&selectFields=isSeries&selectFields=year&selectFields=rating&selectFields=budget&selectFields=movieLength&selectFields=genres&selectFields=countries&selectFields=top250&selectFields=description&selectFields=shortDescription&notNullFields=top250&sortField=top250&sortType=1&typeNumber=1&rating.kp=7.2-10"),
//             //https://api.kinopoisk.dev/v1.4/movie?page=1&limit=250&selectFields=id&selectFields=name&selectFields=alternativeName&selectFields=type&selectFields=isSeries&selectFields=year&selectFields=rating&selectFields=budget&selectFields=movieLength&selectFields=genres&selectFields=countries&selectFields=top250&selectFields=description&selectFields=shortDescription&notNullFields=top250&sortField=top250&sortType=1&typeNumber=1&rating.kp=7.2-10
//             Headers =
//             {
//                 { "accept", "application/json" },
//                 { "X-API-KEY",  ""},
//             },
//         };
//         using (var response = await client.SendAsync(request))
//         {
//             response.EnsureSuccessStatusCode();
//             var resp = await response.Content.ReadAsStringAsync();
//             File.AppendAllText(@"ExternalApi/movieJsonWithDescription.json", resp );
//             return resp;
//         }
//     }
//
//     public static async void GetMoviesUrlsCovers(){
//         var client = new HttpClient();
//
//         var linesRead = File.ReadLines("ExternalApi/movie_ids.txt");
//         // int counter = 0;
//         foreach (var lineRead in linesRead)
//         {
//             // counter++;
//             // if(counter > 3)
//             // {
//             //     break;
//             // }
//
//             var request = new HttpRequestMessage
//             {
//                 Method = HttpMethod.Get,
//                 RequestUri = new Uri($"https://api.kinopoisk.dev/v1.4/image?page=1&limit=3&selectFields=movieId&selectFields=url&selectFields=previewUrl&movieId={lineRead}&type=cover"),
//                 //https://api.kinopoisk.dev/v1.4/movie?page=1&limit=250&selectFields=id&selectFields=name&selectFields=alternativeName&selectFields=type&selectFields=isSeries&selectFields=year&selectFields=rating&selectFields=budget&selectFields=movieLength&selectFields=genres&selectFields=countries&selectFields=top250&notNullFields=top250&sortField=top250&sortType=1&typeNumber=1&rating.kp=7.2-10
//                 Headers =
//                 {
//                     { "accept", "application/json" },
//                     { "X-API-KEY",  "A8CBQ2E-1DZMT6Y-J065YW2-0Y5Y7TA"},
//                 },
//             };
//             using (var response = await client.SendAsync(request))
//             {   
//                 try
//                 {
//                     response.EnsureSuccessStatusCode();
//                 }
//                 catch(Exception ex)
//                 {
//                     Console.WriteLine(ex.Message);
//                     Console.WriteLine(lineRead);
//                 }
//                 string resp = await response.Content.ReadAsStringAsync();
//                 
//
//                 File.AppendAllText(@"ExternalApi/posters.json", Environment.NewLine + resp + ",");
//             }
//         }
//
//     }
//
//     // public static async void deleteUselessRows()
//     // {
//     //     string filePath = "posters.json";
//     //     string[] keysToRemove = { "total", "limit", "page", "pages" };
//
//     //     // Читаем все строки, фильтруем и записываем обратно
//     //     var json = File.ReadAllText(filePath);
//     //     var jsonObject = JObject.Parse(json);
//
//     //     // Удаляем указанные ключи
//     //     foreach (var key in keysToRemove)
//     //     {
//     //         jsonObject.Remove(key);
//     //     }
//
//     //     // Сохраняем изменённый JSON обратно в файл
//     //     File.WriteAllText(filePath, jsonObject.ToString());
//     // }
//
// }   