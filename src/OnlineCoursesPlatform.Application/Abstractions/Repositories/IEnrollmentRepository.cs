using OnlineCoursesPlatform.Domain.Entities;
using OnlineCoursesPlatform.Domain.Repositories;

namespace OnlineCoursesPlatform.Application.Abstractions.Repositories
{
    public interface IEnrollmentRepository : IRepositoryEF<Enrollment>
    {
        Task<Enrollment?> GetByUserIdAndCourseIdAsync(int userId, int courseId);
    }
}
