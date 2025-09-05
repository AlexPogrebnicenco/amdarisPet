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
        public async Task<Lesson?> GetByOrderNumberAsync(int courseId, int orderNumber)
        {
            return await _context.Lessons
                .FirstOrDefaultAsync(l => l.CourseId == courseId && l.OrderNumber == orderNumber);
        }

        public async Task<IEnumerable<Lesson>> GetLessonsByCourseIdAsync(int courseId)
        {
            return await _context.Lessons
                .Where(l => l.CourseId == courseId)
                .OrderBy(l => l.OrderNumber)
                .ToListAsync();
        }
    }
}
