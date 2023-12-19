using BookstoreProject.Application;

namespace BookstoreProject.Persistence;

public class UnitofWork : IUnitofWork
{
    private readonly ApplicationDbContext _applicationDbContext;

    public UnitofWork(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task CommitAsync()
    {
        await _applicationDbContext.SaveChangesAsync();
    }

    public void Commit()
    {
        _applicationDbContext.SaveChanges();
    }
}