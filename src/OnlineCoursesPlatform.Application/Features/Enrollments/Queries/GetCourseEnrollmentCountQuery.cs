using MediatR;

namespace OnlineCoursesPlatform.Application.Features.Enrollments.Queries
{
    public record GetCourseEnrollmentCountQuery(int CourseId) : IRequest<int>;

}
