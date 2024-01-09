using AutoMapper;
using BookstoreProject.Application.DTOs;
using BookstoreProject.Application.Services;
using BookstoreProject.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreProject.Api.Controllers;

public class CategoryController : CustomBaseController
{
    private readonly IMapper _mapper;
    private readonly IService<Category> _service;

    public CategoryController(IService<Category> service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }
    
    [HttpPost]
    public async Task<IActionResult> Add(CategoryDto categoryDto)
    {
        var category = await _service.AddAsync(_mapper.Map<Category>(categoryDto));
        var categoriesDto = _mapper.Map<CategoryDto>(category);
        return CreateIActionResult(CustomResponseDto<CategoryDto>.Success(201, categoriesDto));
    }
}