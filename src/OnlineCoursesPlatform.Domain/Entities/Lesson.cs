namespace OnlineCoursesPlatform.Domain.Entities
{
    public class Lesson
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public int OrderNumber { get; set; }

        public int CourseId { get; set; }
        public Course? Course { get; set; }

        public ICollection<ProgressRecord> ProgressRecords { get; set; } = new List<ProgressRecord>();
    }
}
