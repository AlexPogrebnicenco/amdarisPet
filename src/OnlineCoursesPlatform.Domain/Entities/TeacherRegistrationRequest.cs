using OnlineCoursesPlatform.Domain.Enums;

namespace OnlineCoursesPlatform.Domain.Entities
{
    public class TeacherRegistrationRequest
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int Age { get; set; }
        public string Gender { get; set; } = null!;
        public TeacherRequestStatus Status { get; set; } = TeacherRequestStatus.Pending;
        public DateTime RequestedAt { get; set; } = DateTime.UtcNow;
    }
}
