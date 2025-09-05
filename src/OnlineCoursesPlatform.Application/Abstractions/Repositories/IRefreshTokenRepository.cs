using OnlineCoursesPlatform.Domain.Entities;
using OnlineCoursesPlatform.Domain.Repositories;

namespace OnlineCoursesPlatform.Application.Abstractions.Repositories
{
    public interface IRefreshTokenRepository : IRepositoryEF<RefreshToken>
    {
        Task<RefreshToken?> GetByTokenAsync(string token);
        Task RevokeAsync(RefreshToken token);
        Task RevokeAllAsync(int userId);
    }
}
