namespace Domain.Exceptions.NotFoundExceptions;

public class GenreNotFoundException : NotFoundException
{
    public GenreNotFoundException(Guid genreId) 
        : base($"Genre with id: {genreId} doesn't exist in the database.")
    {
    }
    
    public GenreNotFoundException(string name) 
        : base($"Genre with name: {name} doesn't exist in the database.")
    {
    }
} 