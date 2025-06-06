using MediatR;
using OnlineCoursesPlatform.Application.Features.Teachers.Dto;

namespace OnlineCoursesPlatform.Application.Features.Teachers.Commands
{
    public record CreateTeacher(CreateTeacherDto Dto) : IRequest<TeacherDto>;
}
