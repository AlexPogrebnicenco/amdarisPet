using OnlineCoursesPlatform.Domain.Entities;

namespace OnlineCoursesPlatform.Application.Features.Users.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public required string UserName { get; set; }
        public required string Email { get; set; }
    }
}
