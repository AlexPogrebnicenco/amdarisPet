namespace OnlineCoursesPlatform.Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
 
        public User()
        {
        }

        public User(int id, string name, string email, string passwordHash)
        {
            Id = id;
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
        }
    }
}
