namespace OnlineCoursesPlatform.Application.Features.Auth.Dto
{
    public class RegisterDto
    {
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string ConfirmPassword { get; set; } = null!;
        public int Age { get; set; }
        public string Gender { get; set; } = null!;
        public string Role { get; set; } = null!;
    }

}
