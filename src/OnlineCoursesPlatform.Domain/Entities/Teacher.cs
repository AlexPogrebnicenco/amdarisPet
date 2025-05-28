namespace OnlineCoursesPlatform.Domain.Entities
{
    public class Teacher
    {
        public int Id { get; set; }
        public required string TeacherName { get; set; }
        public required string Email { get; set; }
        public ICollection<CourseTeacher> CourseTeachers { get; set; } = new List<CourseTeacher>();
    }
}
