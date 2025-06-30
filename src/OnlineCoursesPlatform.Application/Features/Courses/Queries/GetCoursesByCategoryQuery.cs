using MediatR;
using OnlineCoursesPlatform.Application.Features.Courses.Dto;

namespace OnlineCoursesPlatform.Application.Features.Courses.Queries
{
    public record GetCoursesByCategoryQuery(int CategoryId, int PageNumber, int PageSize) : IRequest<IEnumerable<CourseDto>>;

}
