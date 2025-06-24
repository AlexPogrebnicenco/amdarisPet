using OnlineCoursesPlatform.Domain.Entities;
using OnlineCoursesPlatform.Domain.Repositories;

public interface ISetPasswordTokenRepository : IRepositoryEF<SetPasswordToken>
{
    Task<SetPasswordToken?> GetByTokenAsync(string token);
}
