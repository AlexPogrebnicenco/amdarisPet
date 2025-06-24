using MediatR;
using OnlineCoursesPlatform.Application.Features.TeacherRegistrationRequests.Dto;

namespace OnlineCoursesPlatform.Application.Features.TeacherRegistrationRequests.Queries
{
    public class GetPendingTeacherRequestsQuery : IRequest<List<TeacherRequestDto>>
    {
    }
}
