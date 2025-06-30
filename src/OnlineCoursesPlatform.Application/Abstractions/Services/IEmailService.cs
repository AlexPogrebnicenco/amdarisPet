namespace OnlineCoursesPlatform.Application.Abstractions.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string body);
    }
}
