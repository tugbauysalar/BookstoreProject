using Microsoft.AspNetCore.Identity;

namespace BookstoreProject.Domain.Entities;

public class User : IdentityUser
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public int Id { get; set; }
    public string PhoneNumber { get; set; }
}