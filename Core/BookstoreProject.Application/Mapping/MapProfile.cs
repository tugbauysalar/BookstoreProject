using AutoMapper;
using BookstoreProject.Application.DTOs;
using BookstoreProject.Domain.Entities;

namespace BookstoreProject.Application.Mapping;

public class MapProfile : Profile
{
    public MapProfile()
    {
        CreateMap<ProductEntity, ProductDto>().ReverseMap();
        CreateMap<CategoryEntity, CategoryDto>().ReverseMap();
    }
}