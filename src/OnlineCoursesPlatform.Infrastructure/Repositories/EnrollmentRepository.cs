using Microsoft.EntityFrameworkCore;
using OnlineCoursesPlatform.Application.Abstractions.Repositories;
using OnlineCoursesPlatform.Domain.Entities;
using OnlineCoursesPlatform.Infrastructure.Persistence;

namespace OnlineCoursesPlatform.Infrastructure.Repositories
{
    public class EnrollmentRepository : RepositoryEF<Enrollment>, IEnrollmentRepository
    {
        public EnrollmentRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Enrollment?> GetByUserIdAndCourseIdAsync(int userId, int courseId)
        {
            return await _context.Enrollments
                .FirstOrDefaultAsync(e => e.UserId == userId && e.CourseId == courseId);
        }

        public async Task<IEnumerable<Course>> GetCoursesByUserIdAsync(int userId, int pageNumber, int pageSize)
        {
            return await _context.Enrollments
                .Where(e => e.UserId == userId && e.Course != null)
                .Include(e => e.Course)
                    .ThenInclude(c => c.Category)
                .Include(e => e.Course)
                    .ThenInclude(c => c.Lessons)
                .Include(e => e.Course)
                    .ThenInclude(c => c.CourseAuthors)
                        .ThenInclude(ca => ca.User)
                .OrderByDescending(e => e.EnrolledAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(e => e.Course!)
                .ToListAsync();
        }
        public async Task<int> CountUserEnrollmentsAsync(int userId)
        {
            return await _context.Enrollments
                .CountAsync(e => e.UserId == userId);
        }

        public IQueryable<Course> QueryCoursesByUserId(int userId)
        {
            return _context.Enrollments
                .Where(e => e.UserId == userId && e.Course != null)
                .Include(e => e.Course) 
                    .ThenInclude(c => c.Category)
                .Include(e => e.Course)
                    .ThenInclude(c => c.Lessons)
                .Include(e => e.Course)
                    .ThenInclude(c => c.CourseTags)
                        .ThenInclude(ct => ct.Tag)
                .Include(e => e.Course)
                    .ThenInclude(c => c.CourseAuthors)
                        .ThenInclude(ca => ca.User)
                .Select(e => e.Course!);
        }
        public async Task<int> CountUserEnrolledCoursesAsync(IQueryable<Course> query, CancellationToken cancellationToken)
        {
            return await query.CountAsync(cancellationToken);
        }
        public async Task<List<Course>> GetPagedUserEnrolledCoursesAsync(IQueryable<Course> query, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            return await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
        }
    }
}
