using OnlineCoursesPlatform.Domain.Entities;

namespace OnlineCoursesPlatform.Application.Features.Teachers.Dto
{
    public class TeacherDto
    {
        public int Id { get; set; }
        public required string TeacherName { get; set; }
        public required string Email { get; set; }
    }
}
