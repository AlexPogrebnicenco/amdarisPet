namespace OnlineCoursesPlatform.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }

        public required string CategoryName { get; set; }

        public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

        public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
    }
}
