using System;
using OnlineCoursesPlatform.Application.Repositories;
using OnlineCoursesPlatform.Domain.Models;
using Xunit;

namespace OnlineCoursesPlatform.Tests.Repositories
{
    public class InMemoryRepositoryTests
    {
        private readonly InMemoryRepository<Course> _repository;

        public InMemoryRepositoryTests()
        {
            _repository = new InMemoryRepository<Course>();
        }

        [Fact]
        public void Add_ShouldAddCourse()
        {
            var course = new Course(1, "C# Programming", "Learn to code in C#", "John Doe");

            _repository.Add(course);

            var retrievedCourse = _repository.GetById(1);
            Assert.NotNull(retrievedCourse);
            Assert.Equal(course.Title, retrievedCourse.Title);
        }


        [Fact]
        public void Update_ShouldModifyCourse()
        {
            var course = new Course(1, "C# Programming", "Learn to code in C#", "John Doe");
            _repository.Add(course);

            var updatedCourse = new Course(1, "Advanced C# Programming", "Learn advanced topics in C#", "John Doe");

            _repository.Update(updatedCourse);

            var retrievedCourse = _repository.GetById(1);
            Assert.NotNull(retrievedCourse);
            Assert.Equal(updatedCourse.Title, retrievedCourse.Title);
        }
    }
}
