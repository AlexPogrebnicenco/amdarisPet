namespace OnlineCoursesPlatform.Domain.Entities
{
    public class SetPasswordToken
    {
        public int Id { get; set; }
        public string Token { get; set; } = null!;
        public DateTime ExpiresAt { get; set; }
        public bool IsUsed { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; } = null!;
    }
}
