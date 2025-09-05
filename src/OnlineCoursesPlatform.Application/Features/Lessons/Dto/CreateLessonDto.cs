namespace OnlineCoursesPlatform.Application.Features.Lessons.Dto
{
    public class CreateLessonDto
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int OrderNumber { get; set; }
        public string Content { get; set; } = null!;
        public List<string> Resources { get; set; } = new();
        public List<VideoInfoDto> VideoUrls { get; set; } = new();
    }
}
