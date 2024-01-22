namespace BookstoreProject.Application.DTOs;

public class BookDto
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string AuthorName { get; set; }
    public string Pages { get; set; }
    public string ImageUrl { get; set; }
    public string Description { get; set; }
    public int Id { get; set; }
}