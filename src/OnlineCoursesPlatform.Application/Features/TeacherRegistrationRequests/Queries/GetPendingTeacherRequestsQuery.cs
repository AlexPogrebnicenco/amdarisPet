using MediatR;
using OnlineCoursesPlatform.Application.Common.Dto;
using OnlineCoursesPlatform.Application.Features.TeacherRegistrationRequests.Dto;

namespace OnlineCoursesPlatform.Application.Features.TeacherRegistrationRequests.Queries
{
    public class GetPendingTeacherRequestsQuery : IRequest<PagedResult<TeacherRequestDto>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }

        public GetPendingTeacherRequestsQuery(int page, int pageSize)
        {
            Page = page;
            PageSize = pageSize;
        }
    }
}
