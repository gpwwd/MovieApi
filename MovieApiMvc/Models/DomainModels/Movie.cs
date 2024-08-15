namespace MovieApiMvc.Models.DomainModels
{
public class Movie
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string? AlternativeName { get; private set; }
    public string Type { get; private set; }
    public int Year { get; private set; }
    public Rating? Rating { get; private set; }
    public Budget? Budget { get; private set; }
    public int MovieLength { get; private set; }
    public List<Genre>? Genres { get; private set; }
    public List<Country>? Countries { get; private set; }
    public int Top250 { get; private set; }
    public bool IsSeries { get; private set; }
    public List<User>? FavMovieUsers { get; private set; }
    public List<User>? WatchLaterUsers { get; private set; }
    
    private Movie(Guid id, string name, string? alternativeName, string type, int year, Rating? rating, Budget? budget, 
                  int movieLength, List<Genre>? genres, List<Country>? countries, int top250, bool isSeries, 
                  List<User>? favMovieUsers, List<User>? watchLaterUsers)
    {
        Id = id;
        Name = name;
        AlternativeName = alternativeName;
        Type = type;
        Year = year;
        Rating = rating;
        Budget = budget;
        MovieLength = movieLength;
        Genres = genres;
        Countries = countries;
        Top250 = top250;
        IsSeries = isSeries;
        FavMovieUsers = favMovieUsers;
        WatchLaterUsers = watchLaterUsers;
    }

    public static Movie Create(string name, string type, int year, Rating? rating = null, 
                               Budget? budget = null, string? alternativeName = null, int movieLength = 0, 
                               List<Genre>? genres = null, List<Country>? countries = null, int top250 = 0, 
                               bool isSeries = false, List<User>? favMovieUsers = null, List<User>? watchLaterUsers = null)
    {
        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(type))
            throw new ArgumentException("Name or type is required and cannot be empty.", nameof(name));

        if (name.Length > 100)
            throw new ArgumentException("Name cannot be longer than 100 characters.", nameof(name));

        if (year < 1800 || year > DateTime.Now.Year)
            throw new ArgumentException("Year is not valid.", nameof(year));

        return new Movie(
            id: Guid.NewGuid(),
            name: name,
            alternativeName: alternativeName,
            type: type,
            year: year,
            rating: rating,
            budget: budget,
            movieLength: movieLength,
            genres: genres ?? new List<Genre>(),
            countries: countries ?? new List<Country>(),
            top250: top250,
            isSeries: isSeries,
            favMovieUsers: favMovieUsers ?? new List<User>(),
            watchLaterUsers: watchLaterUsers ?? new List<User>()
        );
    }
}

}