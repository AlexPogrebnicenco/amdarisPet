using MediatR;
using OnlineCoursesPlatform.Application.Features.Cloudinary.Dto;

namespace OnlineCoursesPlatform.Application.Features.Cloudinary.Queries
{
    public record GetCloudinarySignatureQuery : IRequest<CloudinarySignatureResponseDto>
    {
        public string? Caption { get; set; }
        public string ResourceType { get; set; } = "video";
    }
}
