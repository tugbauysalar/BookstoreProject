using AutoMapper;
using BookstoreProject.Application.DTOs;
using BookstoreProject.Application.Services;
using BookstoreProject.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace BookstoreProject.Persistence.Services;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;

    public UserService(UserManager<User> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<CustomResponseDto<UserDto>> CreateUserAsync(UserRegisterDto userRegisterDto)
    {
        var user = new User()
        {
            UserName = userRegisterDto.UserName,
            Email = userRegisterDto.Email,
            NameSurname = userRegisterDto.NameSurname,
            Password = userRegisterDto.Password,
           
        };
        var result = await _userManager.CreateAsync(user, userRegisterDto.Password);
        
        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(x => x.Description).ToList();
            return CustomResponseDto<UserDto>.Error(400, errors);
        }

        return CustomResponseDto<UserDto>.Success(200, _mapper.Map<UserDto>(user));
    }

    public async Task<CustomResponseDto<UserDto>> LoginUserAsync(UserLoginDto userLoginDto)
    {
        var user = await _userManager.FindByEmailAsync(userLoginDto.Email);
        if (user == null)
        {
            return CustomResponseDto<UserDto>.Error(404, "Girilen email kayıtlı değil!");
        }
        return CustomResponseDto<UserDto>.Success(200, _mapper.Map<UserDto>(user));
    }

    public async Task<CustomResponseDto<NoContentDto>> DeleteUserAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return CustomResponseDto<NoContentDto>.Error(404, "Email bulunamadı.");
        }

        await _userManager.DeleteAsync(user);
        return CustomResponseDto<NoContentDto>.Success(204);
    }
}