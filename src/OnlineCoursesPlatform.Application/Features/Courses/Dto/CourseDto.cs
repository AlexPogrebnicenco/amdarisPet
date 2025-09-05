namespace OnlineCoursesPlatform.Application.Features.Courses.Dto
{
    public class CourseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int? CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public string Difficulty { get; set; } = null!;

        public int LessonsCount { get; set; }
        public string? AuthorAvatarUrl { get; set; }
        public int AuthorId { get; set; }
        public string? About { get; set; }
    }

}
