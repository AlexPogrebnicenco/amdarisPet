using MediatR;
using OnlineCoursesPlatform.Application.Features.Courses.Dto;

namespace OnlineCoursesPlatform.Application.Features.Courses.Commands
{
    public record UpdateCourseCommand(int Id, UpdateCourseDto Dto) : IRequest<Unit>;

}
