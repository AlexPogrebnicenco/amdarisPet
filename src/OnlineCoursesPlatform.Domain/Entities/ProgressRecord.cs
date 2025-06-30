namespace OnlineCoursesPlatform.Domain.Entities
{
    public class ProgressRecord
    {
        public int UserId { get; set; }
        public virtual User? User { get; set; }

        public int LessonId { get; set; }
        public virtual Lesson? Lesson { get; set; }

        public bool Completed { get; set; }
        public DateTime? CompletedAt { get; set; }
    }

}
