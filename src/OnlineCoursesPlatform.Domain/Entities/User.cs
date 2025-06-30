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

        public string Role { get; set; } = "User";
        public string? ExternalProvider { get; set; }

        public bool IsApproved { get; set; } = false;

        public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public virtual ICollection<Certificate> Certificates { get; set; } = new List<Certificate>();
        public virtual ICollection<ProgressRecord> ProgressRecords { get; set; } = new List<ProgressRecord>();
        public virtual ICollection<SetPasswordToken> SetPasswordTokens { get; set; } = new List<SetPasswordToken>();
        public virtual ICollection<CourseAuthor> CourseAuthors { get; set; } = new List<CourseAuthor>();
    }

}
