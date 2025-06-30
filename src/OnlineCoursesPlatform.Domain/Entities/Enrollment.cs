namespace OnlineCoursesPlatform.Domain.Entities
{
    public class Enrollment
    {
        public int UserId { get; set; }
        public virtual User? User { get; set; }

        public int CourseId { get; set; }
        public virtual Course? Course { get; set; }

        public DateTime EnrolledAt { get; set; }
    }

}
