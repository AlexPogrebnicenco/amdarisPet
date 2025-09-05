namespace OnlineCoursesPlatform.Application.Features.Lessons.Dto
{
    public class LessonListDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int CourseId { get; set; }
        public int OrderNumber { get; set; }
    }

}
