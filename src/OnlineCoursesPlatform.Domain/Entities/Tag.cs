namespace OnlineCoursesPlatform.Domain.Entities
{
    public class Tag
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public int CategoryId { get; set; } 

        public virtual Category Category { get; set; } = null!;

        public virtual ICollection<CourseTag> CourseTags { get; set; } = new List<CourseTag>();
    }
}
