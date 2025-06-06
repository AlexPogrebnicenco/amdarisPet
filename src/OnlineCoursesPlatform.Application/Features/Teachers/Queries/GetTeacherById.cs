using MediatR;
using OnlineCoursesPlatform.Application.Features.Teachers.Dto;

namespace OnlineCoursesPlatform.Application.Features.Teachers.Queries
{
    public record GetTeacherById(int Id) : IRequest<TeacherDto?>;
}
