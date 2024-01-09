using AutoMapper;
using BookstoreProject.Application.DTOs;
using BookstoreProject.Application.Services;
using Microsoft.EntityFrameworkCore;

namespace BookstoreProject.Persistence.Services;

public class ProductService : IProductService
{
    private readonly ApplicationDbContext _appDbContext;
    private readonly IMapper _mapper;

    public ProductService(ApplicationDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    public async Task<BookDto> GetByNameAsync(string name)
    {
        var product = await _appDbContext.Products.FirstOrDefaultAsync(x => x.Name == name || x.AuthorName == name);
        var bookDto = _mapper.Map<BookDto>(product);
        return bookDto;
    }
}