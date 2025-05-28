namespace OnlineCoursesPlatform.Domain.Entities
{
    public class Tag
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public ICollection<CourseTag> CourseTags { get; set; } = new List<CourseTag>();
    }
}
