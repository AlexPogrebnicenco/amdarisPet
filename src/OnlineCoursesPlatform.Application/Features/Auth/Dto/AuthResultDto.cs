namespace OnlineCoursesPlatform.Application.Features.Auth.Dto
{
    public class AuthResultDto
    {
        public string Token { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public DateTime Expiration { get; set; }
    }
}
