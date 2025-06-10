using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineCoursesPlatform.Domain.Entities;
using OnlineCoursesPlatform.Domain.Repositories;

namespace OnlineCoursesPlatform.Application.Abstractions.Repositories
{
    public interface IRefreshTokenRepository : IRepositoryEF<RefreshToken>
    {
        Task<RefreshToken?> GetByTokenAsync(string token);
        Task RevokeAsync(RefreshToken token);
    }
}
