using OnlineCoursesPlatform.Domain.Entities;

namespace OnlineCoursesPlatform.Application.Features.Courses.Dto
{
    public class CourseDto
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required ICollection<Teacher> Teachers { get; set; }
        public required ICollection<Review> Reviews { get; set; }
        public DateTime DateCreated { get; set; }

        public static CourseDto FromCourse(Course course)
        {
            return new CourseDto
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
                Teachers = course.Teachers,
                Reviews = course.Reviews
            };
        }
    }
}
