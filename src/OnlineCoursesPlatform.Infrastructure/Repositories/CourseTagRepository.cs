using Microsoft.EntityFrameworkCore;
using OnlineCoursesPlatform.Application.Abstractions.Repositories;
using OnlineCoursesPlatform.Domain.Entities;
using OnlineCoursesPlatform.Infrastructure.Persistence;

namespace OnlineCoursesPlatform.Infrastructure.Repositories
{
    public class CourseTagRepository : RepositoryEF<CourseTag>, ICourseTagRepository
    {
        public CourseTagRepository(AppDbContext context) : base(context)
        {
        }
    }
}
