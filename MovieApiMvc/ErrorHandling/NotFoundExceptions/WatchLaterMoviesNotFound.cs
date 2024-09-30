namespace MovieApiMvc.ErrorHandling.NotFoundExceptions;

public class WatchLaterMoviesNotFound : NotFoundException
{
    public WatchLaterMoviesNotFound(Guid[] movieIds)
        :base ($"The movie with ids: {string.Join(", ", movieIds)} not found or were already added to watch later list.")
    {
    }
}