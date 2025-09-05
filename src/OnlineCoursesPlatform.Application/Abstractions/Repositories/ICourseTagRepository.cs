using OnlineCoursesPlatform.Domain.Entities;
using OnlineCoursesPlatform.Domain.Repositories;

namespace OnlineCoursesPlatform.Application.Abstractions.Repositories
{
    public interface ICourseTagRepository : IRepositoryEF<CourseTag>
    {
        Task<List<CourseTag>> GetByCourseIdAsync(int courseId);
    }
}
