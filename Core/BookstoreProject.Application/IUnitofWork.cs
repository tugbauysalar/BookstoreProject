namespace BookstoreProject.Application;

public interface IUnitofWork
{
    Task CommitAsync();
    void Commit();
}