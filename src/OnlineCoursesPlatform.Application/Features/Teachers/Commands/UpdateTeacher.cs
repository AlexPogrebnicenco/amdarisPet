using MediatR;
using OnlineCoursesPlatform.Application.Features.Teachers.Dto;

namespace OnlineCoursesPlatform.Application.Features.Teachers.Commands
{
    public record UpdateTeacher(int Id, UpdateTeacherDto Dto) : IRequest<TeacherDto?>;
}
