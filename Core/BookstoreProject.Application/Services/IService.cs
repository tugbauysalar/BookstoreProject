using System.Linq.Expressions;
using BookstoreProject.Application.DTOs;

namespace BookstoreProject.Application.Services;

public interface IService<TEntity> where TEntity : class
{
    Task<TEntity> GetByIdAsync(int id);
    IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression);
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity> AddAsync(TEntity entity);
    Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
    Task DeleteAll(IEnumerable<TEntity> entities);
}