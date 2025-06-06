using MediatR;

namespace OnlineCoursesPlatform.Application.Features.Teachers.Commands
{
    public record DeleteTeacher(int Id) : IRequest<bool>;
}
