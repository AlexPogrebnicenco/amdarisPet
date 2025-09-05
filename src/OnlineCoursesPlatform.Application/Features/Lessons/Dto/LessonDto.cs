namespace OnlineCoursesPlatform.Application.Features.Lessons.Dto
{
    public class LessonDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int OrderNumber { get; set; }
        public int CourseId { get; set; }
        public string Content { get; set; } = null!;
        public List<string> Resources { get; set; } = new();
        public List<VideoInfoDto> VideoUrls { get; set; } = new();
    }
}
