using Microsoft.EntityFrameworkCore;
using OnlineCoursesPlatform.Application.Abstractions.Repositories;
using OnlineCoursesPlatform.Domain.Entities;
using OnlineCoursesPlatform.Infrastructure.Persistence;

namespace OnlineCoursesPlatform.Infrastructure.Repositories
{
    public class CourseRepository : RepositoryEF<Course>, ICourseRepository
    {
        public CourseRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Course>> GetMostPopularAsync(int pageNumber, int pageSize)
        {
            var query = _context.Courses
                .OrderByDescending(c => c.Enrollments.Count);

            return await GetPagedAsync(query, pageNumber, pageSize);
        }

        public async Task<IEnumerable<Course>> GetLastCreatedAsync(int pageNumber, int pageSize)
        {
            var query = _context.Courses
                .OrderByDescending(c => c.DateCreated);

            return await GetPagedAsync(query, pageNumber, pageSize);
        }

        public async Task<IEnumerable<Course>> GetLastModifiedAsync(int pageNumber, int pageSize)
        {
            var query = _context.Courses
                .OrderByDescending(c => c.DateModified);

            return await GetPagedAsync(query, pageNumber, pageSize);
        }

        public async Task<IEnumerable<Course>> GetLongestAsync(int pageNumber, int pageSize)
        {
            var query = _context.Courses
                .OrderByDescending(c => c.Lessons.Count);

            return await GetPagedAsync(query, pageNumber, pageSize);
        }

        public async Task<IEnumerable<Course>> GetShortestAsync(int pageNumber, int pageSize)
        {
            var query = _context.Courses
                .OrderBy(c => c.Lessons.Count);

            return await GetPagedAsync(query, pageNumber, pageSize);
        }

        public async Task<IEnumerable<Course>> GetCoursesByTagsAsync(List<string> tags, int pageNumber, int pageSize)
        {
            var query = _context.Courses
                .Include(c => c.CourseTags)
                    .ThenInclude(ct => ct.Tag)
                .Where(c => c.CourseTags.Any(ct => ct.Tag != null && tags.Contains(ct.Tag.Name)))
                .OrderByDescending(c => c.Enrollments.Count);

            return await GetPagedAsync(query, pageNumber, pageSize);
        }

        public async Task<IEnumerable<Course>> GetAlphabeticallyAscAsync(int pageNumber, int pageSize)
        {
            var query = _context.Courses
                .OrderBy(c => c.Title);

            return await GetPagedAsync(query, pageNumber, pageSize);
        }

        public async Task<IEnumerable<Course>> GetAlphabeticallyDescAsync(int pageNumber, int pageSize)
        {
            var query = _context.Courses
                .OrderByDescending(c => c.Title);

            return await GetPagedAsync(query, pageNumber, pageSize);
        }

        public async Task<IEnumerable<Course>> GetCoursesByCategoryAsync(int categoryId, int pageNumber, int pageSize)
        {
            var query = _context.Courses
                .Where(c => c.CategoryId == categoryId)
                .OrderByDescending(c => c.Enrollments.Count);

            return await GetPagedAsync(query, pageNumber, pageSize);
        }

    }
}
