using OnlineCoursesPlatform.Domain.Entities;
using OnlineCoursesPlatform.Domain.Repositories;

namespace OnlineCoursesPlatform.Application.Abstractions.Repositories
{
    public interface ICourseRepository : IRepositoryEF<Course>
    {
        Task<IEnumerable<Course>> GetMostPopularAsync(int pageNumber, int pageSize);
        Task<IEnumerable<Course>> GetLastCreatedAsync(int pageNumber, int pageSize);
        Task<IEnumerable<Course>> GetLastModifiedAsync(int pageNumber, int pageSize);
        Task<IEnumerable<Course>> GetLongestAsync(int pageNumber, int pageSize);
        Task<IEnumerable<Course>> GetShortestAsync(int pageNumber, int pageSize);
        Task<IEnumerable<Course>> GetCoursesByTagsAsync(List<string> tags, int pageNumber, int pageSize);
        Task<IEnumerable<Course>> GetAlphabeticallyAscAsync(int pageNumber, int pageSize);
        Task<IEnumerable<Course>> GetAlphabeticallyDescAsync(int pageNumber, int pageSize);
        Task<IEnumerable<Course>> GetCoursesByCategoryAsync(int categoryId, int pageNumber, int pageSize);

    }

}
