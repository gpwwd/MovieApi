﻿using System.Linq.Expressions;

namespace MovieApiMvc.DataBaseAccess.Repositories.Contracts;

public interface IRepositoryBase<T>
{
    IQueryable<T> FindAll(bool trackChanges);

    IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges);

    Task CreateAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
}