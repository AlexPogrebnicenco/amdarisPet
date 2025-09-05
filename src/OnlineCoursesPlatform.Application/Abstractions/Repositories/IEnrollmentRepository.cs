using OnlineCoursesPlatform.Domain.Entities;
using OnlineCoursesPlatform.Domain.Repositories;

namespace OnlineCoursesPlatform.Application.Abstractions.Repositories
{
    public interface IEnrollmentRepository : IRepositoryEF<Enrollment>
    {
        Task<Enrollment?> GetByUserIdAndCourseIdAsync(int userId, int courseId);
        Task<IEnumerable<Course>> GetCoursesByUserIdAsync(int userId, int pageNumber, int pageSize);
        Task<int> CountUserEnrollmentsAsync(int userId);
        IQueryable<Course> QueryCoursesByUserId(int userId);
        Task<int> CountUserEnrolledCoursesAsync(IQueryable<Course> query, CancellationToken cancellationToken);
        Task<List<Course>> GetPagedUserEnrolledCoursesAsync(IQueryable<Course> query, int pageNumber, int pageSize, CancellationToken cancellationToken);
    }
}
