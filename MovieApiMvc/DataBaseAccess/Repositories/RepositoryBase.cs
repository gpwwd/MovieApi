using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MovieApiMvc.DataBaseAccess.Repositories.Contracts;

namespace MovieApiMvc.DataBaseAccess.Repositories;

public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
{ 
    protected MovieDataBaseContext _context;

    protected RepositoryBase(MovieDataBaseContext context)
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
    
    public async Task CreateAsync(T entity) => await _context.Set<T>().AddAsync(entity);
    public void Update(T entity) => _context.Set<T>().Update(entity);
    /// <summary>
    /// Правильно настроить каскдное удаление сущностей в конфигурациях фильмов
    /// </summary>>
    public void Delete(T entity) => _context.Set<T>().Remove(entity);
}