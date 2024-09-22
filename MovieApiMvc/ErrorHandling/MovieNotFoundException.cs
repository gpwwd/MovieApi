namespace MovieApiMvc.ErrorHandling;

public sealed class MovieNotFoundException : NotFoundException
{
    public MovieNotFoundException(Guid movieId)
        :base ($"The movie with id: {movieId} doesn't exist in the database.")
    {
    }
}