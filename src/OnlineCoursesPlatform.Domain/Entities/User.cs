namespace OnlineCoursesPlatform.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public required string UserName { get; set; }
        public required string Email { get; set; }

        public string? Password { get; set; }
        public int? Age { get; set; }
        public string? Gender { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public ICollection<Certificate> Certificates { get; set; } = new List<Certificate>();   
        public ICollection<ProgressRecord> ProgressRecords { get; set; } = new List<ProgressRecord>();
    }
}
