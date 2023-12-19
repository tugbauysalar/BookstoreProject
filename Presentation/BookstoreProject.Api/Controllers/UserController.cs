using BookstoreProject.Application.DTOs;
using BookstoreProject.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreProject.Api.Controllers;
[Route("api/[controller]/[action]")]
public class UserController : CustomBaseController
{
    private readonly IUserService _userService;

    
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(UserRegisterDto userRegisterDto)
    {
        return CreateIActionResult(await _userService.CreateUserAsync(userRegisterDto));
    }

    [HttpPost]
    public async Task<IActionResult> LoginUser(UserLoginDto userLoginDto)
    {
        return CreateIActionResult(await _userService.LoginUserAsync(userLoginDto));
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteUser(string email)
    {
        return CreateIActionResult(await _userService.DeleteUserAsync(email));
    }
}