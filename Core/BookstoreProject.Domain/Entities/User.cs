using Microsoft.AspNetCore.Identity;

namespace BookstoreProject.Domain.Entities;

public class User : IdentityUser
{
    public string? Surname { get; set; }
    public string Password { get; set; }
}