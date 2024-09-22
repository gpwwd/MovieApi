using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MovieApiMvc.DataBaseAccess.Repositories.Contracts;

namespace MovieApiMvc.DataBaseAccess.Repositories;

public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
{ 
    protected MovieDataBaseContext RepositoryContext;
    
    public RepositoryBase(MovieDataBaseContext context)
        => RepositoryContext = context;

    public IQueryable<T> FindAll(bool trackChanges) =>
        trackChanges
            ? RepositoryContext.Set<T>()
            : RepositoryContext.Set<T>()//DbSet<TEntity> Возвращает экземпляр для доступа к сущностям заданного типа в контексте и базовом хранилище.
                .AsNoTracking();
    
    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges) =>
        !trackChanges ?
            RepositoryContext.Set<T>()
                .Where(expression)
                .AsNoTracking() :
            RepositoryContext.Set<T>()
                .Where(expression);
    
    public void Create(T entity) => RepositoryContext.Set<T>().Add(entity);
    public void Update(T entity) => RepositoryContext.Set<T>().Update(entity);
    public void Delete(T entity) => RepositoryContext.Set<T>().Remove(entity);
}