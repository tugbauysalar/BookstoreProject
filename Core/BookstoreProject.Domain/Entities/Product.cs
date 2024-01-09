using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mime;
using Microsoft.AspNetCore.Http;

namespace BookstoreProject.Domain.Entities;

public class Product
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public decimal Stock { get; set; }
    public int Id { get; set; }
    public string AuthorName { get; set; }
    public string Pages { get; set; }
    public int CategoryId { get; set; }
    public string ImgName { get; set; }
    [NotMapped]
    public IFormFile Image { get; set; }
    
    
}