namespace EduLink.Application.Authentication.Dtos;

public class RefreshTokenResult
{
    public string AccessToken { get; set; } = default!;
    public DateTime AccessTokenExpiration { get; set; }
    public string RefreshToken { get; set; } = default!;
}
