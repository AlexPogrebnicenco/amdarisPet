using MediatR;
using OnlineCoursesPlatform.Application.Features.Courses.Dto;

namespace OnlineCoursesPlatform.Application.Features.Courses.Commands
{
    public record PatchCourseCommand(int Id, UpdateCourseDto PatchedDto) : IRequest<Unit>;
}
