namespace OnlineCoursesPlatform.Domain.Entities
{
    public class Teacher
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required ICollection<Course> Courses { get; set; }
    }
}
