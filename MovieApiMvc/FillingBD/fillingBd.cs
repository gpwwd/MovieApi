// using (StreamReader r = new StreamReader("ExternalApi/MovieJson.json"))
// {
//     var serviceScope = app.Services.CreateScope();
//     var services = serviceScope.ServiceProvider;
//     var context = services.GetRequiredService<MovieDataBaseContext>();
//     IRepositoryManager manager = new RepositoryManager(context);
//     string json = r.ReadToEnd();
//     MovieApiMvc.DataBaseAccess.Entities.MovieEntities.RootObject items = 
//         JsonConvert.DeserializeObject<MovieApiMvc.DataBaseAccess.Entities.MovieEntities.RootObject>(json);
//     foreach (var item in items.docs)
//     {
//         item.Id = Guid.NewGuid();
//     }
// }