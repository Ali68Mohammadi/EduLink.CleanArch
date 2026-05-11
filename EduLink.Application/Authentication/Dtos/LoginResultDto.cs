namespace EduLink.Application.Authentication.Dtos
{
    public class LoginResultDto
    {
        public string AccessToken { get; set; } = default!;
        public DateTime AccessTokenExpiration { get; set; }
        public string RefreshToken { get; set; } = default!;
    }
}
