using MediatR;
using OnlineCoursesPlatform.Application.Features.Courses.Dto;

namespace OnlineCoursesPlatform.Application.Features.Courses.Commands
{
    public record CreateCourseCommand(CreateCourseDto Dto, int UserId) : IRequest<int>;
}
