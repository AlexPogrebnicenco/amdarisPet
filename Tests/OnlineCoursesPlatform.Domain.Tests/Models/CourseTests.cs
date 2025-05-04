using Xunit;
using OnlineCoursesPlatform.Domain.Models;

namespace OnlineCoursesPlatform.Domain.Tests.Models
{
    public class CourseTests
    {
        [Fact]
        public void Course_ShouldInitializeCorrectly()
        {
            var course = new Course(1, "Course Title", "Course Description", "CourseTeacher");

            Assert.Equal(1, course.Id);
            Assert.Equal("Course Title", course.Title);
            Assert.Equal("Course Description", course.Description);
            Assert.Equal("CourseTeacher", course.Teacher);
        }

        [Fact]
        public void Constructor_ShouldThrowArgumentException_WhenTitleIsNullOrEmpty()
        {
            string invalidTitle = "";
            string validDescription = "Valid Description";
            string validTeacher = "John Doe";

            Assert.Throws<ArgumentException>(() => new Course(1, invalidTitle, validDescription, validTeacher));
        }

        [Fact]
        public void Constructor_ShouldSetDateCreatedToCurrentDate()
        {
            int expectedId = 1;
            string expectedTitle = "C# Programming";
            string expectedDescription = "Learn C# from scratch.";
            string expectedTeacher = "John Doe";

            var course = new Course(expectedId, expectedTitle, expectedDescription, expectedTeacher);

            Assert.True(course.DateCreated <= DateTime.Now);
        }
    }
}