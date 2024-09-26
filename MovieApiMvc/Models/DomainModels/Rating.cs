namespace MovieApiMvc.Models.DomainModels;
public class Rating
{
    public Guid Id { get; private set; }
    public short? Kp { get;private set; }
    public short? Imdb { get;private set; }
    public short? FilmCritics { get;private set; }
    public short? RussianFilmCritics { get;private set; }
    public Guid MovieId { get; private set; }
    public Movie? Movie { get; private set; }
    private Rating(Guid id, short? kp, short? imdb, short? filmCritics, short? russianFilmCritics, Guid movieId, Movie? movie)
    {
        Id = id;
        Kp = kp;
        Imdb = imdb;
        FilmCritics = filmCritics;
        RussianFilmCritics = russianFilmCritics;
    }

    public static Rating Create(Guid movieId, short? kp = 0, short? imdb = 0, short? filmCritics = 0, short? russianFilmCritics = 0, Movie? movie = null)
    {
        if (kp is null && imdb is null && filmCritics is null && russianFilmCritics is null)
        {
            throw new ArgumentException("At least one critic's rating should not be empty.");
        }

        return new Rating(
            id: Guid.NewGuid(),
            movieId: movieId,
            kp: kp ?? 0,
            imdb: imdb ?? 0, 
            filmCritics: filmCritics ?? 0,
            russianFilmCritics: russianFilmCritics ?? 0,
            movie: movie
        );
    }
}
