namespace OnlineCoursesPlatform.Domain.Models
{
    public class Lesson
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int CourseId { get; set; }
        public Course? Course { get; set; }

        public Lesson(int id, string title, string content, int courseId, Course course)
        {
            Id = id;
            Title = title;
            Content = content;
            CourseId = courseId;
            Course = course;
        }
    }
}
