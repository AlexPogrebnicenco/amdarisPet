using OnlineCoursesPlatform.Domain.Entities;
using OnlineCoursesPlatform.Domain.Repositories;

namespace OnlineCoursesPlatform.Application.Abstractions.Repositories
{
    public interface ICourseRepository : IRepositoryEF<Course>
    {
    }
}
