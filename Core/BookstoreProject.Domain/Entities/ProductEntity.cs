using System.Net.Mime;

namespace BookstoreProject.Domain.Entities;

public class ProductEntity
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public decimal Stock { get; set; }
    public int Id { get; set; }
    
}