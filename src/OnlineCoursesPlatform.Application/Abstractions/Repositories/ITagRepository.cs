using OnlineCoursesPlatform.Domain.Entities;
using OnlineCoursesPlatform.Domain.Repositories;

namespace OnlineCoursesPlatform.Application.Abstractions.Repositories
{
    public interface ITagRepository : IRepositoryEF<Tag>
    {
        Task<List<Tag>> GetAllOrderedAsync();
    }
}
