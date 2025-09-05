namespace OnlineCoursesPlatform.Application.Features.Auth.Dto
{
    public class CreateUserFromGoogleDto
    {
        public string Email { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string? AvatarUrl { get; set; }
    }
}
