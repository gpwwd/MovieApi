using MovieApiMvc.DataBaseAccess.Context;
using MovieApiMvc.DataBaseAccess.Repositories;
using MovieApiMvc.ErrorHandling;
using MovieApiMvc.ExternalApi;
using Newtonsoft.Json;

namespace MovieApiMvc.FillingBD
{
    public class FillBDWithPosters
    {
        public async static void addPosters(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var imagesInfoList = Deserializer.GetMoviesUrlsCovers();

                RootObject movies = new RootObject();
                using (StreamReader r = new StreamReader("ExternalApi/MovieJson.json"))
                {
                    var serviceScope = app.Services.CreateScope();
                    var services = serviceScope.ServiceProvider;
                    var scopedService = services.GetRequiredService<MovieDataBaseContext>();
                    MoviesRepository movRep = new MoviesRepository(scopedService);
                    string json = r.ReadToEnd();
                    movies = JsonConvert.DeserializeObject<RootObject>(json);
                }

                var moviesRepository = scope.ServiceProvider.GetRequiredService<MoviesRepository>();

                foreach(var movie in movies.docs)
                {
                    
                    try
                    {
                        Guid? dbId = await moviesRepository.GetIdByName(movie.Name);
                        if(dbId is null)
                        {
                            Console.WriteLine("no movie with this name");
                            continue;
                        }

                        ImageInfo image = GetImageInfoByMovieJsonId(movie.Id, imagesInfoList);
                        
                        if(image == null)
                        {
                            continue;
                        }

                        await moviesRepository.PutPoster(dbId, image);
                    }
                    catch(EntityNotFoundException ex)
                    {
                        continue;
                    }

                }
            }
        }

        static ImageInfo GetImageInfoByMovieJsonId(int id, List<ImageInfo> images)
        {
            foreach(var image in images)
            {
                if(image.MovieId == id)
                {
                    return image;
                }
            }
            return null;
        }
        public class Movie
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
        public class RootObject
        {
            public List<Movie>? docs { get; set; }
        }
    }
}