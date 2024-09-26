using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MovieApiMvc.DataBaseAccess.Repositories.Contracts;

namespace MovieApiMvc.DataBaseAccess.Repositories;

public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
{ 
    protected MovieDataBaseContext _context;
    
    public RepositoryBase(MovieDataBaseContext context)
        => _context = context;

    public IQueryable<T> FindAll(bool trackChanges) =>
        trackChanges
            ? _context.Set<T>()
            : _context.Set<T>()//DbSet<TEntity> Возвращает экземпляр для доступа к сущностям заданного типа в контексте и базовом хранилище.
                .AsNoTracking();
    
    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges) =>
        !trackChanges ?
            _context.Set<T>()
                .Where(expression)
                .AsNoTracking() :
            _context.Set<T>()
                .Where(expression);
    
    public void Create(T entity)
    {
        var entries2 = _context.ChangeTracker.Entries();
        _context.Set<T>().Add(entity);
        var entries3 = _context.ChangeTracker.Entries();
    }

    public void Update(T entity) => _context.Set<T>().Update(entity);
    public void Delete(T entity) => _context.Set<T>().Remove(entity);
}