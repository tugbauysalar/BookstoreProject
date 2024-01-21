using BookstoreProject.Application.DTOs;

namespace BookstoreProject.Application.Services;

public interface IProductService
{
    Task<List<BookDto>> GetByKeywordAsync(string keyword);
    Task<BookDto> AddDescriptionAsync(int id, string description);
}