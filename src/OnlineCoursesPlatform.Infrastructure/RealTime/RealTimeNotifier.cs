using Microsoft.AspNetCore.SignalR;
using OnlineCoursesPlatform.Application.Abstractions.Services;

namespace OnlineCoursesPlatform.Infrastructure.RealTime
{
    public class RealTimeNotifier : IRealTimeNotifier
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public RealTimeNotifier(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendEnrollmentNotification(int teacherId, string courseTitle, string userEmail, DateTime enrolledAt)
        {
            await _hubContext.Clients
                .Group($"Teacher_{teacherId}")
                .SendAsync("ReceiveEnrollmentNotification", new
                {
                    CourseTitle = courseTitle,
                    UserEmail = userEmail,
                    EnrolledAt = enrolledAt
                });
        }
    }
}
