namespace OnlineCoursesPlatform.Application.Settings
{
    public class EmailSettings
    {
        public string SmtpServer { get; set; } = null!;
        public int Port { get; set; }
        public string SenderEmail { get; set; } = null!;
        public string AppPassword { get; set; } = null!;
    }
}
