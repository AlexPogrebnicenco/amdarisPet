using OnlineCoursesPlatform.Domain.Entities;

namespace OnlineCoursesPlatform.Application.Features.Users.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public required string UserName { get; set; }
        public required string Email { get; set; }

        public static UserDto FromUser(User user)
        {
            return new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
            };
        }
    }
}
