namespace MovieApiMvc.Models.DomainModels;
public class Rating
{
    public Guid Id { get; private set; }
    public double? Kp { get;private set; }
    public double? Imdb { get;private set; }
    public double? FilmCritics { get;private set; }
    public double? RussianFilmCritics { get;private set; }
    public Guid MovieId { get; private set; }
    public Movie? Movie { get; private set; }
    private Rating(Guid id, double? kp, double? imdb, double? filmCritics, double? russianFilmCritics, Guid movieId, Movie? movie)
    {
        Id = id;
        Kp = kp;
        Imdb = imdb;
        FilmCritics = filmCritics;
        RussianFilmCritics = russianFilmCritics;
    }

    public static Rating Create(Guid movieId, double? kp = 0, double? imdb = 0, double? filmCritics = 0, double? russianFilmCritics = 0, Movie? movie = null)
    {
        if (kp is null && imdb is null && filmCritics is null && russianFilmCritics is null)
        {
            throw new ArgumentException("At least one critic's rating should not be empty.");
        }

        return new Rating(
            id: Guid.NewGuid(),
            movieId: movieId,
            kp: kp,
            imdb: imdb, 
            filmCritics: filmCritics,
            russianFilmCritics: russianFilmCritics,
            movie: movie
        );
    }
}
