namespace Domain.Exceptions;

public class EntityAlreadyExistsException : BadRequestException
{
    public EntityAlreadyExistsException(string message) : base(message)
    {
    }
} 