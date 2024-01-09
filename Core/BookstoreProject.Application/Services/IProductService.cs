using BookstoreProject.Application.DTOs;

namespace BookstoreProject.Application.Services;

public interface IProductService
{
    Task<BookDto> GetByNameAsync(string name);
}