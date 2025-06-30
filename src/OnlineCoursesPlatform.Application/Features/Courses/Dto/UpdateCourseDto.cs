namespace OnlineCoursesPlatform.Application.Features.Courses.Dto
{
    public class UpdateCourseDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int? CategoryId { get; set; }
        public List<int>? TagIds { get; set; }
    }
}
