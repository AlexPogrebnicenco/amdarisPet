using MediatR;
using OnlineCoursesPlatform.Application.Common.Dto;
using OnlineCoursesPlatform.Application.Features.Courses.Dto;

namespace OnlineCoursesPlatform.Application.Features.Enrollments.Queries
{
    public record GetUserEnrolledCoursesQuery(
        int UserId,
        int PageNumber,
        int PageSize,
        string? Sort = null,
        string? Tag = null,
        string? Search = null
    ) : IRequest<PagedResult<CourseDto>>;
}
