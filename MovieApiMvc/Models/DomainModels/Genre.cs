namespace MovieApiMvc.Models.DomainModels;

public class Genre
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public List<Movie>? Movies { get; private set; }

    private Genre(Guid id, string name, List<Movie>? movies)
    {
        Id = id;
        Name = name;
        Movies = movies ?? new List<Movie>();
    }

    public static Genre Create(string name, List<Movie>? movies = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Genre name is required and cannot be empty.", nameof(name));

        if (name.Length > 50)
            throw new ArgumentException("Genre name cannot be longer than 50 characters.", nameof(name));

        return new Genre(
            id: Guid.NewGuid(),
            name: name,
            movies: movies
        );
    }

    public void AddMovie(Movie movie)
    {
        if (movie == null)
            throw new ArgumentNullException(nameof(movie), "Movie cannot be null.");

        Movies?.Add(movie);
    }

    public void RemoveMovie(Movie movie)
    {
        if (movie == null)
            throw new ArgumentNullException(nameof(movie), "Movie cannot be null.");

        Movies?.Remove(movie);
    }
}

