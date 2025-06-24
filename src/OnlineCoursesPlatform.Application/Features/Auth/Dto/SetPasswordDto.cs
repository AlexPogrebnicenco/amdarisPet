namespace OnlineCoursesPlatform.Application.Features.Auth.Dto
{
    public class SetPasswordDto
    {
        public string Token { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string ConfirmPassword { get; set; } = null!;
    }
}
