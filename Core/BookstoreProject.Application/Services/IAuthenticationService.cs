using BookstoreProject.Application.DTOs;

namespace BookstoreProject.Application.Services;

public interface IAuthenticationService
{
    Task<CustomResponseDto<TokenDto>> CreateTokenAsync(UserLoginDto userLoginDto);
    Task<CustomResponseDto<TokenDto>> CreateRefreshTokenAsync(string refreshToken);
    Task<CustomResponseDto<NoContentDto>> RevokeRefreshToken(string refreshToken);
}