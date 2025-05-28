using OnlineCoursesPlatform.Domain.Entities;
using OnlineCoursesPlatform.Domain.Repositories;

namespace OnlineCoursesPlatform.Application.Interfaces.Repositories
{
    public interface IUserRepository : IRepositoryEF<User>
    {
        Task<User?> GetByEmailAsync(string email);
    }
}
