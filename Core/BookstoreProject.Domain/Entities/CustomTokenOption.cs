namespace BookstoreProject.Domain.Entities;

public class CustomTokenOption
{
    public string SectionName { get; set; }
    public string Audience { get; set; }
    public string Issuer { get; set; }
    public int AccessTokenExpiration { get; set; }
    public int RefreshTokenExpiration { get; set; }
    public string SecurityKey { get; set; }
}