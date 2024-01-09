using Microsoft.AspNetCore.Http;

namespace BookstoreProject.Application.DTOs;

public class ProductDto
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string AuthorName { get; set; }
    public string Pages { get; set; }
    public int Stock { get; set; }
    public int CategoryId { get; set; }
    public string ImgName { get; set; }
    public IFormFile Image { get; set; }
}