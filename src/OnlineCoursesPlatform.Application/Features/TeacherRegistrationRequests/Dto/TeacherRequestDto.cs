namespace OnlineCoursesPlatform.Application.Features.TeacherRegistrationRequests.Dto
{
    public class TeacherRequestDto
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int Age { get; set; }
        public string Gender { get; set; } = null!;
        public string Status { get; set; } = null!;
        public DateTime RequestedAt { get; set; }
    }
}