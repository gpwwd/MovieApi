﻿using MovieApiMvc.DataBaseAccess.Repositories.Contracts;

namespace MovieApiMvc.Services.Interfaces;

public interface IRepositoryManager
{
    IMovieRepository MovieRepository { get; }
    IUserRepository UserRepository { get; }
    void Save();
}