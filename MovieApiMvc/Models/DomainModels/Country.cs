namespace MovieApiMvc.Models.DomainModels;

public class Country
{
    public Guid Id { get; private set; }
    public string? Name { get; private set; }
    public List<Movie>? Movies { get; private set; }

    private Country(Guid id, string? name, List<Movie>? movies)
    {
        Id = id;
        Name = name;
        Movies = movies ?? new List<Movie>();
    }

    public static Country Create(string? name, List<Movie>? movies = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be null or empty.", nameof(name));


        return new Country(
            id: Guid.NewGuid(),
            name: name,
            movies: movies ?? new List<Movie>()
        );
    }
}

