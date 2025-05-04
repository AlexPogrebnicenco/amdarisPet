namespace OnlineCoursesPlatform.Domain.Models
{
    public class Subscription
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }

        public User? User { get; set; }

        public Subscription(int userId, DateTime startDate, DateTime endDate, bool isActive)
        {
            UserId = userId;
            StartDate = startDate;
            EndDate = endDate;
            IsActive = isActive;
        }
    }
}
