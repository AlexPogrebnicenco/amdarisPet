using OnlineCoursesPlatform.Domain.Entities;
using OnlineCoursesPlatform.Domain.Repositories;

namespace OnlineCoursesPlatform.Application.Abstractions.Repositories
{
    public interface ITeacherRegistrationRequestRepository : IRepositoryEF<TeacherRegistrationRequest>
    {
        Task<TeacherRegistrationRequest?> GetByEmailAsync(string email);
        Task<(List<TeacherRegistrationRequest> Items, int TotalCount)> GetPendingRequestsAsync(int page, int pageSize);
    }
}
