namespace OnlineCoursesPlatform.Domain.Entities
{
    public class Lesson
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public int OrderNumber { get; set; }

        public required string Content { get; set; } // Markdown или текст
        public List<string> Resources { get; set; } = new(); // Ссылки на материалы
        public List<VideoInfo> VideoUrls { get; set; } = new();

        public int CourseId { get; set; }
        public virtual Course? Course { get; set; }

        public virtual ICollection<ProgressRecord> ProgressRecords { get; set; } = new List<ProgressRecord>();
    }
}
