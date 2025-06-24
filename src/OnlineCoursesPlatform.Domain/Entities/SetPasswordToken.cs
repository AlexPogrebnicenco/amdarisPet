namespace OnlineCoursesPlatform.Domain.Entities
{
    public class SetPasswordToken
    {
        public int Id { get; set; }
        public string Token { get; set; } = null!;
        public DateTime ExpiresAt { get; set; }
        public bool IsUsed { get; set; }

        // Связь с User
        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }

}
