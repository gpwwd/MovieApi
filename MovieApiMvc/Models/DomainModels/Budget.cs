namespace MovieApiMvc.Models.DomainModels;
public class Budget
{
    public Guid Id { get; private set; }
    public string? Currency { get; private set; }
    public double Value { get; private set; }
    public Guid MovieId { get; private set; }
    public Movie? Movie { get; private set; }

    private Budget(Guid id, string? currency, double value, Guid movieId, Movie? movie)
    {
        Id = id;
        Currency = currency;
        Value = value;
        MovieId = movieId;
        Movie = movie;
    }

    public static Budget Create(string? currency, double value, Guid movieId, Movie? movie = null)
    {
        if (value < 0)
            throw new ArgumentException("Value must be non-negative.", nameof(value));

        return new Budget(
            id: Guid.NewGuid(),
            currency: currency,
            value: value,
            movieId: movieId,
            movie: movie
        );
    }
}

