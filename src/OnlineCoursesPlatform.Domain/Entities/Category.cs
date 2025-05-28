namespace OnlineCoursesPlatform.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public required string CategoryName { get; set; }

        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
