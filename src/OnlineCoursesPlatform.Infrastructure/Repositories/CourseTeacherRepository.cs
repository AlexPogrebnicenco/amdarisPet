using Microsoft.EntityFrameworkCore;
using OnlineCoursesPlatform.Application.Abstractions.Repositories;
using OnlineCoursesPlatform.Domain.Entities;
using OnlineCoursesPlatform.Infrastructure.Persistence;

namespace OnlineCoursesPlatform.Infrastructure.Repositories
{
    public class CourseTeacherRepository : RepositoryEF<CourseTeacher>, ICourseTeacherRepository
    {
        public CourseTeacherRepository(AppDbContext context) : base(context)
        {
        }
    }
}
