using BookstoreProject.Application.DTOs;
using BookstoreProject.Application.Services;
using Microsoft.EntityFrameworkCore;

namespace BookstoreProject.Persistence.Services;

public class ProductService : IProductService
{
    private readonly ApplicationDbContext _applicationDbContext;

    public ProductService(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<CustomResponseDto<BookDto>> GetByNameAsync(string name)
    {
        throw new NotImplementedException();
    }
}