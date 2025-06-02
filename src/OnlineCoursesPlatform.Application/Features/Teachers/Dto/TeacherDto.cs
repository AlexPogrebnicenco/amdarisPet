using OnlineCoursesPlatform.Domain.Entities;

namespace OnlineCoursesPlatform.Application.Features.Teachers.Dto
{
    public class TeacherDto
    {
        public int Id { get; set; }
        public required string TeacherName { get; set; }
        public required string Email { get; set; }

        public static TeacherDto FromTeacher(Teacher teacher)
        {
            return new TeacherDto
            {
                Id = teacher.Id,
                TeacherName = teacher.TeacherName,
                Email = teacher.Email
            };
        }
    }
}
