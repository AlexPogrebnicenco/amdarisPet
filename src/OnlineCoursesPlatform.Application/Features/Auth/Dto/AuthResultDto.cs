namespace OnlineCoursesPlatform.Application.Features.Auth.Dto
{
    public class AuthResultDto
    {
        public string AccessToken { get; set; } = null!;
        public DateTime AccessTokenExpiration { get; set; }

        public string RefreshToken { get; set; } = null!;
        public DateTime RefreshTokenExpiration { get; set; }

        public string Email { get; set; } = null!;
        public string UserName { get; set; } = null!;
    }
}
