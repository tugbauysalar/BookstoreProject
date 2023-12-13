
using BookstoreProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookstoreProject.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<ProductEntity> ProductEntities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<ProductEntity>().HasData(
            new ProductEntity
            {

            }
        );
    }
}