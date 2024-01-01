using System.Linq.Expressions;

namespace BookstoreProject.Application;

public interface IRepository<TEntity> where TEntity : class
{
    Task<TEntity> GetByIdAsync(int id);
    IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression);
    IQueryable<TEntity> GetAll();
    Task AddAsync(TEntity entity);
    Task AddRangeAsync(IEnumerable<TEntity> entities);
    void Update(TEntity entity);
    void Delete(TEntity entity);
    void DeleteAll(IEnumerable<TEntity> entities);
}