namespace OnlineCoursesPlatform.Application.Abstractions.Services
{
    public interface IRealTimeNotifier
    {
        Task SendEnrollmentNotification(int teacherId, string courseTitle, string userEmail, DateTime enrolledAt);
    }
}
