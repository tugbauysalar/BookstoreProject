using AutoMapper;
using BookstoreProject.Application.DTOs;
using BookstoreProject.Application.Services;
using BookstoreProject.Domain.Entities;
using BookstoreProject.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace BookstoreProject.Api.Controllers;
[Route("api/[controller]/[action]")]

public class ProductController : CustomBaseController
{
    private readonly IMapper _mapper;
    private readonly IService<Product> _service;
    private readonly IProductService _productService;
    private readonly ApplicationDbContext _applicationDbContext;
    
    public ProductController(IService<Product> service, IMapper mapper, IProductService productService, ApplicationDbContext
        applicationDbContext)
    {
        _service = service;
        _mapper = mapper;
        _productService = productService;
        _applicationDbContext = applicationDbContext;
    }
    
    [HttpPost]
    public async Task<IActionResult> Add(ProductDto productDto)
    {
        var product = await _service.AddAsync(_mapper.Map<Product>(productDto));
        var productsDto = _mapper.Map<ProductDto>(product);
        return CreateIActionResult(CustomResponseDto<ProductDto>.Success(201, productsDto));
    }

    [HttpGet("{keyword}")]
    public async Task<IActionResult> GetProductByKeyword(string keyword)
    {
        var products = await _productService.GetByKeywordAsync(keyword);
        var productDto = _mapper.Map<List<BookDto>>(products.ToList());

        if (productDto.Count > 0) 
        {
            return CreateIActionResult(CustomResponseDto<List<BookDto>>.Success(200, productDto));
        }

        var error = "Aradığınız kitap veya yazar bulunamadı!";
        return CreateIActionResult(CustomResponseDto<List<BookDto>>.Error(404, error));
    }

    [HttpGet]
    public async Task<IActionResult> All()
    {
        var products = await _service.GetAllAsync();
        var productDto = _mapper.Map<List<BookDto>>(products.ToList());
        return CreateIActionResult(CustomResponseDto<List<BookDto>>.Success(200, productDto));
    }
    
    [HttpPut]
    public async Task<IActionResult> Update(ProductDto productDto)
    {
        await _service.UpdateAsync(_mapper.Map<Product>(productDto));
        return CreateIActionResult(CustomResponseDto<NoContentDto>.Success(204));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var product = await _service.GetByIdAsync(id);
        await _service.DeleteAsync(product);
        return CreateIActionResult(CustomResponseDto<NoContentDto>.Success(204));
    }
    
    [HttpPost("{id}/upload-photo")]
    public async Task<IActionResult> UploadPhoto([FromForm]int id, IFormFile photo)
    {
        var product = await _service.GetByIdAsync(id);

        if (product == null)
        {
            return NotFound(); 
        }

        if (photo != null && photo.Length > 0)
        {
            using (var memoryStream = new MemoryStream())
            {
                await photo.CopyToAsync(memoryStream);
                byte[] photoBytes = memoryStream.ToArray();
                product.ImageUrl = Convert.ToBase64String(photoBytes); 
            }
        }

        await _service.UpdateAsync(product); 

        return Ok(); 
    }

    [HttpPost("{id}/adddescription")]
    public async Task<IActionResult> AddDescription(int id, string description)
    {
        var product = await _productService.AddDescriptionAsync(id, description);
        var bookDto = _mapper.Map<BookDto>(product);
        return Ok(bookDto);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(int id)
    {
        var product = await _service.GetByIdAsync(id);
        var bookDto = _mapper.Map<BookDto>(product);
        return Ok(bookDto);
    }
    
    private static List<CartDto> cart = new List<CartDto>();

    [HttpPost("{id}")]
    public async Task<IActionResult> AddToCart(int id)
    {
        var product = await _service.GetByIdAsync(id);
        var cartDto = _mapper.Map<CartDto>(product);
        cart.Add(cartDto);
        return CreateIActionResult(CustomResponseDto<NoContentDto>.Success(204));
    }
    
    [HttpGet]
    public async Task<IActionResult> GetCart()
    {
        return CreateIActionResult(CustomResponseDto<List<CartDto>>.Success(200, cart));
    }
    
} 