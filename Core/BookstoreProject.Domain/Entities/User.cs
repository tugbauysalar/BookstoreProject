using Microsoft.AspNetCore.Identity;

namespace BookstoreProject.Domain.Entities;

public class User : IdentityUser
{
    public string? NameSurname { get; set; }
    public string Password { get; set; }
}