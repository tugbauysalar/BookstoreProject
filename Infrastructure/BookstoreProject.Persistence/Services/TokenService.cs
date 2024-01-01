using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BookstoreProject.Application.DTOs;
using BookstoreProject.Application.Services;
using BookstoreProject.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace BookstoreProject.Persistence.Services;

public class TokenService : ITokenService
{
    private readonly CustomTokenOption _customTokenOption;

    public TokenService(IOptions<CustomTokenOption> options)
    {
        _customTokenOption = options.Value;
    }

    private string CreateRefreshToken()
    {
        return Guid.NewGuid().ToString();
    }
    
    private IEnumerable<Claim> GetClaim(User user)
    {

        var userList = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(ClaimTypes.Name, user.UserName)
        };

        return userList;

    }
    public TokenDto CreateToken(User user)
    {
        var accessTokenExpiration = DateTime.Now.AddMinutes(_customTokenOption.AccessTokenExpiration);
        var refreshTokenExpiration = DateTime.Now.AddMinutes(_customTokenOption.RefreshTokenExpiration);
        var securityKey = SignService.GetSymmetricSecurityKey(_customTokenOption.SecurityKey);
        SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
        
        JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
            issuer: _customTokenOption.Issuer,
            audience: _customTokenOption.Audience,
            expires: accessTokenExpiration,
            notBefore:  DateTime.Now,
            claims: GetClaim(user),
            signingCredentials: signingCredentials);

        var handler = new JwtSecurityTokenHandler();
        var token = handler.WriteToken(jwtSecurityToken);

        var tokenDto = new TokenDto
        {
            AccessToken = token,
            RefreshToken = CreateRefreshToken(),
            AccessTokenExpiration = accessTokenExpiration,
            RefreshTokenExpiration = refreshTokenExpiration
        };

        return tokenDto;
    }
}