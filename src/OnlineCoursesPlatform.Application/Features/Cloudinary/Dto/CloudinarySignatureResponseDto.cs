namespace OnlineCoursesPlatform.Application.Features.Cloudinary.Dto
{
    public class CloudinarySignatureResponseDto
    {
        public string Signature { get; set; } = null!;
        public long Timestamp { get; set; }
        public string CloudName { get; set; } = null!;
        public string ApiKey { get; set; } = null!;
    }
}
