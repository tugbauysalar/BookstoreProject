
using BookstoreProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookstoreProject.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Product> ProductEntities { get; set; }
    public DbSet<Category> CategoryEntities { get; set; }
    public DbSet<Users>UsersEntities { get; set; }
 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        /*
        modelBuilder.Entity<CategoryEntity>().HasData(
            new CategoryEntity
            {

            }
        );*/
    }
}