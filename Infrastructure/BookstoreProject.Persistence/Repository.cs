using System.Linq.Expressions;
using BookstoreProject.Application;
using Microsoft.EntityFrameworkCore;

namespace BookstoreProject.Persistence;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly ApplicationDbContext _appDbContext;
    private readonly DbSet<TEntity> _dbSet;

    public Repository(ApplicationDbContext appDbContext)
    {
        _appDbContext = appDbContext;
        _dbSet = _appDbContext.Set<TEntity>();
    }
    public async Task AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
    }
    public async Task AddRangeAsync(IEnumerable<TEntity> entities)
    {
        await _dbSet.AddRangeAsync(entities);
    }
    public void Delete(TEntity entity)
    {
        _dbSet.Remove(entity);
    }
    public void DeleteAll(IEnumerable<TEntity> entities)
    {
        _dbSet.RemoveRange(entities);
    }
    public IQueryable<TEntity> GetAll()
    {
        return _dbSet.AsNoTracking().AsQueryable();
    }
    public async Task<TEntity> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public void Update(TEntity entity)
    {
        _dbSet.Update(entity);
    }
    public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression)
    {
        return _dbSet.Where(expression);
    }
}