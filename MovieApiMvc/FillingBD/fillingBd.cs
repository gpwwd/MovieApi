        // using (StreamReader r = new StreamReader("MovieJson.json"))
        // {
        //     var serviceScope = app.Services.CreateScope();
        //     var services = serviceScope.ServiceProvider;
        //     var scopedService = services.GetRequiredService<MovieDataBaseContext>();
        //     MoviesRepository movRep = new MoviesRepository(scopedService);
        //     string json = r.ReadToEnd();
        //     MovieApiMvc.DataBaseAccess.Entities.RootObject items = JsonConvert.DeserializeObject<MovieApiMvc.DataBaseAccess.Entities.RootObject>(json);
        //     foreach (var movie in items.docs){
        //         movRep.Add(movie.Id, movie.Name, movie.AlternativeName, movie.Type,
        //                                         movie.Year, movie.Rating, movie.MovieLength, movie.Genres,
        //                                         movie.Countries, movie.Budget, movie.Top250, movie.IsSeries);
        //     }
        // }
