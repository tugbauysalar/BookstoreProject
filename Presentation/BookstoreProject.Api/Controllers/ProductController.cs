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
    public static IWebHostEnvironment _webHostEnvironment;
    
    public ProductController(IService<Product> service, IMapper mapper, IProductService productService, IWebHostEnvironment
        webHostEnvironment)
    {
        _service = service;
        _mapper = mapper;
        _productService = productService;
        _webHostEnvironment = webHostEnvironment;
    }
    
    [HttpPost]
    public async Task<IActionResult> Add([FromForm] ProductDto productDto)
    {
        try
        {
            if (productDto.File.Length > 0)
            {
                string path = _webHostEnvironment.WebRootPath + "\\uploads\\";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                using (FileStream fileStream = System.IO.File.Create(path + productDto.File.FileName))
                {
                    productDto.File.CopyTo(fileStream);
                    fileStream.Flush();
                }
            }
            var product = await _service.AddAsync(_mapper.Map<Product>(productDto));
            var productsDto = _mapper.Map<ProductDto>(product);
            return CreateIActionResult(CustomResponseDto<ProductDto>.Success(201, productsDto));
        }
        catch (Exception e)
        {
            return CreateIActionResult(CustomResponseDto<ProductDto>.Error(500, e.Message));
        }
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

    [HttpGet]
    public async Task<IActionResult> All()
    {
        var products = await _service.GetAllAsync();
        var productDto = _mapper.Map<List<BookDto>>(products.ToList());
        return CreateIActionResult(CustomResponseDto<List<BookDto>>.Success(200, productDto));
    }

}