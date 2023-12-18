
using BookstoreProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookstoreProject.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<User> Users { get; set; }
 
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