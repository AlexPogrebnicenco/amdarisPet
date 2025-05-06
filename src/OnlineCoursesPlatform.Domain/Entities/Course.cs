namespace OnlineCoursesPlatform.Domain.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required ICollection<Teacher> Teachers { get; set; }
        public required ICollection<Review> Reviews { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
