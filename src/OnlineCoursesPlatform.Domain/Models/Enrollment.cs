namespace OnlineCoursesPlatform.Domain.Models
{
    public class Enrollment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CourseId { get; set; }
        public DateTime EnrollmentDate { get; set; }

        public User? User { get; set; }
        public Course? Course { get; set; }

        public Enrollment(int userId, int courseId, DateTime enrollmentDate)
        {
            UserId = userId;
            CourseId = courseId;
            EnrollmentDate = enrollmentDate;
        }
    }
}

