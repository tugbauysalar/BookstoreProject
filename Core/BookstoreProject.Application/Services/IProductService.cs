using BookstoreProject.Application.DTOs;

namespace BookstoreProject.Application.Services;

public interface IProductService
{
    Task<CustomResponseDto<BookDto>> GetByNameAsync(string name);
}