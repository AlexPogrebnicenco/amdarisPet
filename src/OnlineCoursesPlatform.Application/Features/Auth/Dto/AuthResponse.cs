using OnlineCoursesPlatform.Application.Features.Users.Dto;

namespace OnlineCoursesPlatform.Application.Features.Auth.Dto
{
    public class AuthResponse
    {
        public string AccessToken { get; set; } = null!;
        public DateTime AccessTokenExpiration { get; set; }

        public string RefreshToken { get; set; } = null!;
        public DateTime RefreshTokenExpiration { get; set; }

        public UserAccountInfoDto UserInfo { get; set; } = null!;
    }
}
