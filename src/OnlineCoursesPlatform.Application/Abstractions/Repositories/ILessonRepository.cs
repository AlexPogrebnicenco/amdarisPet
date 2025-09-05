using OnlineCoursesPlatform.Domain.Entities;
using OnlineCoursesPlatform.Domain.Repositories;

namespace OnlineCoursesPlatform.Application.Abstractions.Repositories
{
    public interface ILessonRepository : IRepositoryEF<Lesson>
    {
        Task<Lesson?> GetByOrderNumberAsync(int courseId, int orderNumber);
        Task<IEnumerable<Lesson>> GetLessonsByCourseIdAsync(int courseId);
    }
}
