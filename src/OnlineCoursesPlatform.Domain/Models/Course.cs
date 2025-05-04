namespace OnlineCoursesPlatform.Domain.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Teacher { get; set; }
        public DateTime DateCreated { get; set; }

        public Course(int id, string title, string description, string teacher)
        {
            if (string.IsNullOrEmpty(title)) throw new ArgumentException("Title cannot be empty", nameof(title));
            if (string.IsNullOrEmpty(description)) throw new ArgumentException("Description cannot be empty", nameof(description));

            Id = id;
            Title = title;
            Description = description;
            Teacher = teacher;
            DateCreated = DateTime.Now;
        }
    }
}
