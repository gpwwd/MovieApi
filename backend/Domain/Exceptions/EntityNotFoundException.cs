namespace Domain.Exceptions;

using Domain.Exceptions.NotFoundExceptions;

public class EntityNotFoundException : NotFoundException
{
    public EntityNotFoundException(string message) : base(message)
    {
    }
} 