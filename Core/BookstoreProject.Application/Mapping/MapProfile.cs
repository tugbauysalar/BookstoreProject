using AutoMapper;
using BookstoreProject.Application.DTOs;
using BookstoreProject.Domain.Entities;

namespace BookstoreProject.Application.Mapping;

public class MapProfile : Profile
{
    public MapProfile()
    {
        CreateMap<Product, ProductDto>().ReverseMap();
        CreateMap<Product, BookDto>().ReverseMap();
        CreateMap<Product, CartDto>().ReverseMap();
        CreateMap<Category, CategoryDto>().ReverseMap();
        CreateMap<User, UserRegisterDto>().ReverseMap();
        CreateMap<User, UserLoginDto>().ReverseMap();
        CreateMap<User, UserDto>().ReverseMap();
    }
}