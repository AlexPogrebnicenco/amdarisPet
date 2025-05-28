using System.ComponentModel;

namespace OnlineCoursesPlatform.Domain.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        public int? CategoryId { get; set; }
        public Category? Category { get; set; }


        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public ICollection<CourseTeacher> CourseTeachers { get; set; } = new List<CourseTeacher>();
        public ICollection<CourseTag> CourseTags { get; set; } = new List<CourseTag>();
        public ICollection<Certificate> Certificates { get; set; } = new List<Certificate>();

    }
}
