using BookstoreProject.Application;
using BookstoreProject.Application.DTOs;
using BookstoreProject.Application.Services;
using BookstoreProject.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookstoreProject.Persistence.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly ITokenService _tokenService;
    private readonly UserManager<User> _userManager;
    private readonly IUnitofWork _unitOfWork;
    private readonly IRepository<UserRefreshToken> _userRefreshTokenService;
    
    public AuthenticationService(ITokenService tokenService, UserManager<User> userManager, IUnitofWork unitOfWork,
    IRepository<UserRefreshToken> userRefreshTokenService)
    {
        _tokenService = tokenService;
        _userManager = userManager;
        _unitOfWork = unitOfWork;
        _userRefreshTokenService = userRefreshTokenService;
    }

    public async Task<CustomResponseDto<TokenDto>> CreateTokenAsync(UserLoginDto userLoginDto)
    {
        if (userLoginDto == null) throw new ArgumentNullException(nameof(userLoginDto));

        var user = await _userManager.FindByEmailAsync(userLoginDto.Email);
        if (user == null) return CustomResponseDto<TokenDto>.Error(400, "E-posta veya şifre yanlış!");

        if (!await _userManager.CheckPasswordAsync(user, userLoginDto.Password))
        {
            return CustomResponseDto<TokenDto>.Error(400, "E-posta veya şifre yanlış!");
        }

        var token = _tokenService.CreateToken(user);

        var userRefreshToken = await _userRefreshTokenService.Where(x => x.UserId == user.Id).SingleOrDefaultAsync();
        if (userRefreshToken == null)
        {
            await _userRefreshTokenService.AddAsync(new UserRefreshToken
            {
                UserId = user.Id,
                RefreshToken = token.RefreshToken,
                Expiration = token.RefreshTokenExpiration.ToUniversalTime()
            });
        }
        else
        {
            userRefreshToken.RefreshToken = token.RefreshToken;
            userRefreshToken.Expiration = token.RefreshTokenExpiration.ToUniversalTime();
        }
    
        await _unitOfWork.CommitAsync();

        return CustomResponseDto<TokenDto>.Success(200, token);
    }

    public async Task<CustomResponseDto<TokenDto>> CreateRefreshTokenAsync(string refreshToken)
    {
        var existRefreshToken = await _userRefreshTokenService.Where(x => x.RefreshToken == refreshToken).FirstOrDefaultAsync();
        if (existRefreshToken == null)
        {
            return CustomResponseDto<TokenDto>.Error(404, "Refresh token bulunamadı!");
        }

        var user = await _userManager.FindByIdAsync(existRefreshToken.UserId);
        if (user == null)
        {
            return CustomResponseDto<TokenDto>.Error(404, "Kullanıcı bulunamadı!");
        }

        var tokenDto = _tokenService.CreateToken(user);

        existRefreshToken.RefreshToken = tokenDto.RefreshToken;
        existRefreshToken.Expiration = tokenDto.RefreshTokenExpiration.ToUniversalTime(); 

        await _unitOfWork.CommitAsync();

        return CustomResponseDto<TokenDto>.Success(200, tokenDto);
    }

   

    public async Task<CustomResponseDto<NoContentDto>> RevokeRefreshToken()
    {
        await _unitOfWork.CommitAsync();

        return CustomResponseDto<NoContentDto>.Success(200);
    }
}
