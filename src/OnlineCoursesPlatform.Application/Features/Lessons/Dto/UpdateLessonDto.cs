namespace OnlineCoursesPlatform.Application.Features.Lessons.Dto
{
    public class UpdateLessonDto
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Content { get; set; } = null!;
        public List<string> Resources { get; set; } = new();
        public List<VideoInfoDto> VideoUrls { get; set; } = new();
    }
}
