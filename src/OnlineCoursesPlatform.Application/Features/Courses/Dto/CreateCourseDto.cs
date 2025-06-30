namespace OnlineCoursesPlatform.Application.Features.Courses.Dto
{
    public class CreateCourseDto
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int? CategoryId { get; set; }

        public List<int> TagIds { get; set; } = new();
    }
}
