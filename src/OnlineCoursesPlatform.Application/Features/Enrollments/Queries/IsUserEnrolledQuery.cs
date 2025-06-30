using MediatR;

namespace OnlineCoursesPlatform.Application.Features.Enrollments.Queries
{
    public record IsUserEnrolledQuery(int UserId, int CourseId) : IRequest<bool>;

}
