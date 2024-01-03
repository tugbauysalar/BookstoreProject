using System.Runtime.InteropServices.JavaScript;
using BookstoreProject.Application.DTOs;
using BookstoreProject.Application.Services;
using BookstoreProject.Application.Validations;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreProject.Api.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]

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
        var validationResult = new UserRegisterDtoValidator().Validate(userRegisterDto);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            return BadRequest(new {errors});
        }
        return CreateIActionResult(await _userService.CreateUserAsync(userRegisterDto));
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteUser(string userName)
    {
        return CreateIActionResult(await _userService.DeleteUserAsync(userName));
    }
}