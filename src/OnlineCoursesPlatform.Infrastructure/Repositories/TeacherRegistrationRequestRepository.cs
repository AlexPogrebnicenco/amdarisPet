using Microsoft.EntityFrameworkCore;
using OnlineCoursesPlatform.Application.Abstractions.Repositories;
using OnlineCoursesPlatform.Domain.Entities;
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

        public async Task<List<TeacherRegistrationRequest>> GetPendingRequestsAsync()
        {
            return await _context.TeacherRegistrationRequests
                .Where(r => r.Status == "Pending")
                .ToListAsync();
        }
    }
}
