namespace OnlineCoursesPlatform.Application.Features.Tags.Dto
{
    public class TagDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
    }
}
