namespace OnlineCoursesPlatform.Domain.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public required string VoterName { get; set; }
        public short NumStars { get; set; }
        public required string Comment { get; set; }

        public int CourseId { get; set; }
        public virtual Course? Course { get; set; }
    }
}
