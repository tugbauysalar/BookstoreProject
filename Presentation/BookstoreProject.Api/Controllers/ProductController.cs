using AutoMapper;
using BookstoreProject.Application.DTOs;
using BookstoreProject.Application.Services;
using BookstoreProject.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreProject.Api.Controllers;

public class ProductController : CustomBaseController
{
    private readonly IMapper _mapper;
    private readonly IService<Product> _service;
    private readonly IProductService _productService;
    
    public ProductController(IService<Product> service, IMapper mapper, IProductService productService)
    {
        _service = service;
        _mapper = mapper;
        _productService = productService;
    }
    
    [HttpPost]
    public async Task<IActionResult> Add(ProductDto productDto)
    {
        var product = await _service.AddAsync(_mapper.Map<Product>(productDto));
        var productsDto = _mapper.Map<ProductDto>(product);
        return CreateIActionResult(CustomResponseDto<ProductDto>.Success(201, productsDto));
    }

    [HttpGet("{name}")]
    public async Task<IActionResult> GetProductByName(string name)
    {
        var product = await _productService.GetByNameAsync(name);
        var productDto = _mapper.Map<BookDto>(product);
        if (productDto != null)
        {
            return CreateIActionResult(CustomResponseDto<BookDto>.Success(200, productDto));
        }

        var error = "Aradığınız kitap bulunamadı!";
        return CreateIActionResult(CustomResponseDto<BookDto>.Error(404, error));
    }
}