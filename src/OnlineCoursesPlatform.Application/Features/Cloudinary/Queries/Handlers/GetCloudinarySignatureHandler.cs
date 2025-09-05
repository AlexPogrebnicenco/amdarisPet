using MediatR;
using OnlineCoursesPlatform.Application.Features.Cloudinary.Dto;

namespace OnlineCoursesPlatform.Application.Features.Cloudinary.Queries.Handlers
{
    public class GetCloudinarySignatureHandler : IRequestHandler<GetCloudinarySignatureQuery, CloudinarySignatureResponseDto>
    {
        private readonly CloudinaryDotNet.Cloudinary _cloudinary;

        public GetCloudinarySignatureHandler(CloudinaryDotNet.Cloudinary cloudinary)
        {
            _cloudinary = cloudinary;
        }

        public Task<CloudinarySignatureResponseDto> Handle(GetCloudinarySignatureQuery request, CancellationToken cancellationToken)
        {
            var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            var parameters = new SortedDictionary<string, object>
            {
                { "timestamp", timestamp }
            };

            if (!string.IsNullOrWhiteSpace(request.Caption))
            {
                parameters.Add("context", $"caption={request.Caption.Trim()}");
            }

            if (!string.IsNullOrWhiteSpace(request.ResourceType) && request.ResourceType != "video")
            {
                parameters.Add("resource_type", request.ResourceType);
            }

            var signature = _cloudinary.Api.SignParameters(parameters);

            var response = new CloudinarySignatureResponseDto
            {
                Signature = signature,
                Timestamp = timestamp,
                CloudName = _cloudinary.Api.Account.Cloud,
                ApiKey = _cloudinary.Api.Account.ApiKey
            };

            return Task.FromResult(response);
        }
    }
}
