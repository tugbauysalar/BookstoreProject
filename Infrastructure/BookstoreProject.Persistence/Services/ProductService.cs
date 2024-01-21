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

    public async Task<List<BookDto>> GetByKeywordAsync(string keyword)
    {
        var products = await _appDbContext.Products
            .Where(x => EF.Functions.ILike(x.Name, $"%{keyword}%") || EF.Functions.ILike(x.AuthorName, $"%{keyword}%"))
            .ToListAsync();
        var bookDto = _mapper.Map<List<BookDto>>(products.ToList());
        return bookDto;
    }

    public async Task<BookDto> AddDescriptionAsync(int id, string description)
    {
        var product = await _appDbContext.Products.FindAsync(id);
        product.Description = description;
        await _appDbContext.SaveChangesAsync();
        var bookDto = _mapper.Map<BookDto>(product);
        return bookDto;
    }
}