﻿namespace MovieApiMvc.ErrorHandling;

public class UserNotFoundException : NotFoundException
{
    public UserNotFoundException(Guid userId)
        :base ($"The user with id: {userId} doesn't exist in the database.")
    {
    }
}