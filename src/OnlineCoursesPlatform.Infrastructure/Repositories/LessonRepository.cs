using Microsoft.EntityFrameworkCore;
using OnlineCoursesPlatform.Application.Abstractions.Repositories;
using OnlineCoursesPlatform.Domain.Entities;
using OnlineCoursesPlatform.Infrastructure.Persistence;

namespace OnlineCoursesPlatform.Infrastructure.Repositories
{
    public class LessonRepository : RepositoryEF<Lesson>, ILessonRepository
    {
        public LessonRepository(AppDbContext context) : base(context)
        {
        }
    }
}
