using MediatR;
using OnlineCoursesPlatform.Application.Features.Teachers.Dto;

namespace OnlineCoursesPlatform.Application.Features.Teachers.Queries
{
    public record GetAllTeachers(int pageNumber =1, int pageSize = 10) : IRequest<IEnumerable<TeacherDto>>;
}
