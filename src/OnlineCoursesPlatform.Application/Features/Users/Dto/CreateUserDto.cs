namespace OnlineCoursesPlatform.Application.Features.Users.Dto
{
    public class CreateUserDto
    {
        public required string UserName { get; set; }
        public required string Email { get; set; }
    }
}
