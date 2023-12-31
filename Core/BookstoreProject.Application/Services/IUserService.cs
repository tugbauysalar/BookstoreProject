﻿using BookstoreProject.Application.DTOs;

namespace BookstoreProject.Application.Services;

public interface IUserService
{
    Task<CustomResponseDto<UserDto>> CreateUserAsync(UserRegisterDto userRegisterDto);
    Task<CustomResponseDto<NoContentDto>> DeleteUserAsync(string userName);
}