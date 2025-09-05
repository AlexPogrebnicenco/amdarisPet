using Microsoft.EntityFrameworkCore;
using OnlineCoursesPlatform.Application.Abstractions.Repositories;
using OnlineCoursesPlatform.Domain.Entities;
using OnlineCoursesPlatform.Domain.Enums;
using OnlineCoursesPlatform.Infrastructure.Persistence;

namespace OnlineCoursesPlatform.Infrastructure.Repositories
{
    public class TeacherRegistrationRequestRepository : RepositoryEF<TeacherRegistrationRequest>, ITeacherRegistrationRequestRepository
    {
        public TeacherRegistrationRequestRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<TeacherRegistrationRequest?> GetByEmailAsync(string email)
        {
            return await _context.TeacherRegistrationRequests
                .FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<(List<TeacherRegistrationRequest> Items, int TotalCount)> GetPendingRequestsAsync(int page, int pageSize)
        {
            var query = _context.TeacherRegistrationRequests
              .Where(r => r.Status == TeacherRequestStatus.Pending);

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderByDescending(r => r.RequestedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }
    }
}
