using System.Linq.Expressions;
using BookstoreProject.Application;
using BookstoreProject.Application.DTOs;
using BookstoreProject.Application.Services;
using BookstoreProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookstoreProject.Persistence.Services;

public class Service<TEntity> : IService<TEntity> where TEntity : class
{
    private readonly IRepository<TEntity> _repository;
    private readonly IUnitofWork _unitOfWork;

    public Service(IUnitofWork unitOfWork, IRepository<TEntity> repository)
    {
        _unitOfWork = unitOfWork;
        _repository = repository;
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await _repository.AddAsync(entity);
        await _unitOfWork.CommitAsync();
        return entity;
    }

    public async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities)
    {
        await _repository.AddRangeAsync(entities);
        await _unitOfWork.CommitAsync();
        return entities;
    }

    public async Task DeleteAll(IEnumerable<TEntity> entities)
    {
        _repository.DeleteAll(entities);
        await _unitOfWork.CommitAsync();
        
    }

    public async Task DeleteAsync(TEntity entity)
    {
        _repository.Delete(entity);
        await _unitOfWork.CommitAsync();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _repository.GetAll().ToListAsync();
    }

    public async Task<TEntity> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task UpdateAsync(TEntity entity)
    {
        _repository.Update(entity);
        await _unitOfWork.CommitAsync();
    }

    public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression)
    {
        return _repository.Where(expression);
    }
}