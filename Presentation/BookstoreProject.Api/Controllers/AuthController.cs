using BookstoreProject.Application.DTOs;
using BookstoreProject.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreProject.Api.Controllers;
[Route("api/[controller]/[action]")]
public class AuthController : CustomBaseController
{
    private readonly IAuthenticationService _authenticationService;
    public AuthController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }
    
    [HttpPost]
    public async Task<IActionResult> Login(UserLoginDto userLoginDto)
    {
        var result = await _authenticationService.CreateTokenAsync(userLoginDto);
        return CreateIActionResult(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateRefreshToken(RefreshTokenDto refreshTokenDto)
    {
        var result = await _authenticationService.CreateRefreshTokenAsync(refreshTokenDto.Token);
        return CreateIActionResult(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        var result = await _authenticationService.RevokeRefreshToken();
        return CreateIActionResult(result);
    }
}