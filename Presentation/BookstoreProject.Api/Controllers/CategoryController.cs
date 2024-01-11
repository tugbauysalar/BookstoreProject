﻿using AutoMapper;
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
    
    [HttpGet]
    public async Task<IActionResult> All()
    {
        var categories = await _service.GetAllAsync();
        var categoryDto = _mapper.Map<List<CategoryDto>>(categories.ToList());
        return CreateIActionResult(CustomResponseDto<List<CategoryDto>>.Success(200, categoryDto));
    }
    
    [HttpPut]
    public async Task<IActionResult> Update(CategoryDto categoryDto)
    {
        await _service.UpdateAsync(_mapper.Map<Category>(categoryDto));
        return CreateIActionResult(CustomResponseDto<NoContentDto>.Success(204));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var category = await _service.GetByIdAsync(id);
        await _service.DeleteAsync(category);
        return CreateIActionResult(CustomResponseDto<NoContentDto>.Success(204));
    }
}