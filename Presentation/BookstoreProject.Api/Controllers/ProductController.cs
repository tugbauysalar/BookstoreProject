using System.Runtime.InteropServices.JavaScript;
using System.Security.Cryptography;
using BookstoreProject.Domain.Entities;
using BookstoreProject.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BookstoreProject.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController:ControllerBase
{
    private readonly ApplicationDbContext _dbContext;
    private readonly Repository<Product> productRepository;

    [HttpPost]
    public async Task<IActionResult> AddProduct([FromBody]Product product)
    {
        
        await productRepository.AddAsync(product);

        return Ok(product);
    }
    
    // isim ile aratma
    [HttpGet("{name}")]
    public async Task<IActionResult> GetProductByName(String name)
    {
        var getBook = productRepository.GetByNameAsync(name);
        if (getBook == null)
        {
            return NotFound("Bu isimde bir kitap bulunamadı.");
        }
        return Ok(getBook);
    }
    
    
   


}

    
    

