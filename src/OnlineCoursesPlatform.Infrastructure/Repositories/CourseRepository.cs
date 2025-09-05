using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using OnlineCoursesPlatform.Application.Abstractions.Repositories;
using OnlineCoursesPlatform.Application.Features.Courses.Dto;
using OnlineCoursesPlatform.Domain.Entities;
using OnlineCoursesPlatform.Infrastructure.Persistence;

namespace OnlineCoursesPlatform.Infrastructure.Repositories
{
    public class CourseRepository : RepositoryEF<Course>, ICourseRepository
    {
        public CourseRepository(AppDbContext context) : base(context) { }

        public async Task<Course?> GetCourseDetailsByIdAsync(int id)
        {
            return await _context.Courses
                .Include(c => c.Lessons)
                .Include(c => c.Category)
                .Include(c => c.CourseAuthors)
                    .ThenInclude(ca => ca.User)
                .FirstOrDefaultAsync(c => c.Id == id);
        }


        public async Task<IEnumerable<CourseDto>> GetMostPopularAsync(int pageNumber, int pageSize)
        {
            return await _context.Courses
                .OrderByDescending(c => c.Enrollments.Count)
                .Select(ProjectToDto())
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<CourseDto>> GetLastCreatedAsync(int pageNumber, int pageSize)
        {
            return await _context.Courses
                .OrderByDescending(c => c.DateCreated)
                .Select(ProjectToDto())
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<CourseDto>> GetLastModifiedAsync(int pageNumber, int pageSize)
        {
            return await _context.Courses
                .OrderByDescending(c => c.DateModified)
                .Select(ProjectToDto())
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<CourseDto>> GetLongestAsync(int pageNumber, int pageSize)
        {
            return await _context.Courses
                .OrderByDescending(c => c.Lessons.Count)
                .Select(ProjectToDto())
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<CourseDto>> GetShortestAsync(int pageNumber, int pageSize)
        {
            return await _context.Courses
                .OrderBy(c => c.Lessons.Count)
                .Select(ProjectToDto())
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<CourseDto>> GetCoursesByTagsAsync(List<string> tags, int pageNumber, int pageSize)
        {
            return await _context.Courses
                .Where(c => c.CourseTags.Any(ct => ct.Tag != null && tags.Contains(ct.Tag.Name)))
                .OrderByDescending(c => c.Enrollments.Count)
                .Select(ProjectToDto())
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<CourseDto>> GetAlphabeticallyAscAsync(int pageNumber, int pageSize)
        {
            return await _context.Courses
                .OrderBy(c => c.Title)
                .Select(ProjectToDto())
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<CourseDto>> GetAlphabeticallyDescAsync(int pageNumber, int pageSize)
        {
            return await _context.Courses
                .OrderByDescending(c => c.Title)
                .Select(ProjectToDto())
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<CourseDto>> GetCoursesByCategoryAsync(int categoryId, int pageNumber, int pageSize)
        {
            return await _context.Courses
                .Where(c => c.CategoryId == categoryId)
                .OrderByDescending(c => c.Enrollments.Count)
                .Select(ProjectToDto())
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<CourseDto>> GetCoursesByAuthorOrderedByLastModifiedAsync(int authorId, int pageNumber, int pageSize)
        {
            return await _context.Courses
                .Where(c => c.CourseAuthors.Any(ca => ca.UserId == authorId))
                .OrderByDescending(c => c.DateModified)
                .Select(ProjectToDto())
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        private static Expression<Func<Course, CourseDto>> ProjectToDto()
        {
            return c => new CourseDto
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                CategoryId = c.CategoryId,
                CategoryName = c.Category != null
                    ? c.Category.CategoryName!
                    : null,
                DateCreated = c.DateCreated,
                DateModified = c.DateModified,
                Difficulty = c.Difficulty.ToString(),
                About = c.About,
                LessonsCount = c.Lessons.Count,
                AuthorAvatarUrl = c.CourseAuthors
                    .Where(ca => ca.User != null && ca.User.AvatarUrl != null)
                    .Select(ca => ca.User!.AvatarUrl!)
                    .FirstOrDefault()
            };
        }

        public async Task<IEnumerable<CourseDto>> SearchByTitleAsync(string title, int pageNumber, int pageSize)
        {
            return await _context.Courses
                .Where(c => c.Title.ToLower().Contains(title.ToLower()))
                .OrderByDescending(c => c.DateCreated)
                .Select(ProjectToDto()) 
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public IQueryable<Course> QueryCoursesByAuthorId(int userId)
        {
            return _context.Courses
                .Where(c => c.CourseAuthors.Any(ca => ca.UserId == userId))
                .Include(c => c.Lessons)
                .Include(c => c.Category)
                .Include(c => c.CourseTags).ThenInclude(ct => ct.Tag)
                .Include(c => c.CourseAuthors).ThenInclude(ca => ca.User);
        }

        public async Task<int> CountTeacherCoursesAsync(IQueryable<Course> query, CancellationToken cancellationToken)
        {
            return await query.CountAsync(cancellationToken);
        }

        public async Task<List<Course>> GetPagedTeacherCoursesAsync(IQueryable<Course> query, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            return await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
        }
    }
}
