using BookstoreProject.Application.DTOs;
using BookstoreProject.Domain.Entities;

namespace BookstoreProject.Application.Services;

public interface ITokenService
{
    TokenDto CreateToken(User user);
}