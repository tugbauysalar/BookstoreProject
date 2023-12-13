
using BookstoreProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookstoreProject.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<ProductEntity> ProductEntities { get; set; }
    public DbSet<CategoryEntity> CategoryEntities { get; set; }
 
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