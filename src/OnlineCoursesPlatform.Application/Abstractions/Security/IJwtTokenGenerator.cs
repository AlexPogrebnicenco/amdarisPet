namespace OnlineCoursesPlatform.Application.Abstractions.Security
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(int userId, string email, string userName, string role);
        string GenerateRefreshToken();
    }
}
