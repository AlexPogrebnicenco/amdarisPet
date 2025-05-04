namespace OnlineCoursesPlatform.Domain.Models
{
    public class Certificate
    {
        public int Id { get; set; }
        public int EnrollmentId { get; set; }
        public DateTime DateIssued { get; set; }
        public string CertificateUrl { get; set; }

        public Enrollment? Enrollment { get; set; }

        public Certificate(int enrollmentId, DateTime dateIssued, string certificateUrl)
        {
            EnrollmentId = enrollmentId;
            DateIssued = dateIssued;
            CertificateUrl = certificateUrl;
        }
    }
}
