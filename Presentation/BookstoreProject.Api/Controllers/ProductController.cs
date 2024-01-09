using System.Runtime.CompilerServices;
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
    
    public ProductController(IService<Product> service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
     
    }

    [HttpPost]
    public async Task<IActionResult> Add(ProductDto productDto)
    {
        var product = await _service.AddAsync(_mapper.Map<Product>(productDto));
        var productsDto = _mapper.Map<ProductDto>(product);
        return CreateIActionResult(CustomResponseDto<ProductDto>.Success(201, productsDto));
    }
    
    [HttpPost]
    [Route("UploadFile")]
    public async Task<IActionResult> UploadImage([FromForm] ProductDto productDto)
    {
        try
        {
          
            
            string path = Path.Combine("/Images/", productDto.ImgName);
            
            using (Stream stream = new FileStream(path, FileMode.Append))
            {
                await productDto.Image.CopyToAsync(stream);
               
            }
            Console.WriteLine(path);

            return CreateIActionResult(CustomResponseDto<ProductDto>.Success(200, productDto));

        }
        catch(Exception e)
        {
            
        }
        return Ok(productDto);
    }

   
    


}