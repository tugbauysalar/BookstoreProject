using System.Globalization;

namespace BookstoreProject.Application.DTOs;

public class UserRegisterDto
{
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public string UserName { get; set; }
}