namespace Domain.Exceptions.NotFoundExceptions;

public class FavMoviesNotFound : NotFoundException
{
    public FavMoviesNotFound(Guid[] movieIds)
        :base ($"The movie with ids: {string.Join(", ", movieIds)} not found or were already added to fav list.")
    {
    }
}