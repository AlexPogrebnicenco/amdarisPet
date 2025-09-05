using MediatR;
using OnlineCoursesPlatform.Application.Common.Dto;
using OnlineCoursesPlatform.Application.Features.Courses.Dto;

namespace OnlineCoursesPlatform.Application.Features.Courses.Queries
{
    public record GetCoursesByAuthorQuery(
        int AuthorId,
        int PageNumber,
        int PageSize,
        string? Sort,
        string? Tag,
        string? Search
        ) : IRequest<PagedResult<CourseDto>>;
}
