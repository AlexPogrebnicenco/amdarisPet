using OnlineCoursesPlatform.Application.Features.Courses.Dto;
using OnlineCoursesPlatform.Domain.Entities;
using OnlineCoursesPlatform.Domain.Repositories;

namespace OnlineCoursesPlatform.Application.Abstractions.Repositories
{
    public interface ICourseRepository : IRepositoryEF<Course>
    {
        Task<Course?> GetCourseDetailsByIdAsync(int id);


        Task<IEnumerable<CourseDto>> GetMostPopularAsync(int pageNumber, int pageSize);
        Task<IEnumerable<CourseDto>> GetLastCreatedAsync(int pageNumber, int pageSize);
        Task<IEnumerable<CourseDto>> GetLastModifiedAsync(int pageNumber, int pageSize);
        Task<IEnumerable<CourseDto>> GetLongestAsync(int pageNumber, int pageSize);
        Task<IEnumerable<CourseDto>> GetShortestAsync(int pageNumber, int pageSize);
        Task<IEnumerable<CourseDto>> GetCoursesByTagsAsync(List<string> tags, int pageNumber, int pageSize);
        Task<IEnumerable<CourseDto>> GetAlphabeticallyAscAsync(int pageNumber, int pageSize);
        Task<IEnumerable<CourseDto>> GetAlphabeticallyDescAsync(int pageNumber, int pageSize);
        Task<IEnumerable<CourseDto>> GetCoursesByCategoryAsync(int categoryId, int pageNumber, int pageSize);
        Task<IEnumerable<CourseDto>> GetCoursesByAuthorOrderedByLastModifiedAsync(int authorId, int pageNumber, int pageSize);
        Task<IEnumerable<CourseDto>> SearchByTitleAsync(string title, int pageNumber, int pageSize);

        IQueryable<Course> QueryCoursesByAuthorId(int userId);
        Task<int> CountTeacherCoursesAsync(IQueryable<Course> query, CancellationToken cancellationToken);
        Task<List<Course>> GetPagedTeacherCoursesAsync(IQueryable<Course> query, int pageNumber, int pageSize, CancellationToken cancellationToken);

    }
}
