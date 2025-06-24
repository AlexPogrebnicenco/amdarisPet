using Microsoft.EntityFrameworkCore;
using OnlineCoursesPlatform.Domain.Entities;
using OnlineCoursesPlatform.Infrastructure.Persistence;

namespace OnlineCoursesPlatform.Infrastructure.Repositories
{
    public class SetPasswordTokenRepository : RepositoryEF<SetPasswordToken>, ISetPasswordTokenRepository
    {
        public SetPasswordTokenRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<SetPasswordToken?> GetByTokenAsync(string token)
        {
            return await _context.SetPasswordTokens
                .Include(t => t.User) 
                .FirstOrDefaultAsync(t => t.Token == token);
        }
    }
}
