using CloudinaryDotNet.Actions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace OnlineCoursesPlatform.Application.Features.Cloudinary.Commands.Handlers
{
    public class DeleteCloudinaryVideoHandler : IRequestHandler<DeleteCloudinaryVideoCommand, Unit>
    {
        private readonly CloudinaryDotNet.Cloudinary _cloudinary;
        private readonly ILogger<DeleteCloudinaryVideoHandler> _logger;

        public DeleteCloudinaryVideoHandler(CloudinaryDotNet.Cloudinary cloudinary, ILogger<DeleteCloudinaryVideoHandler> logger)
        {
            _cloudinary = cloudinary;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteCloudinaryVideoCommand request, CancellationToken cancellationToken)
        {
            var deletionParams = new DeletionParams(request.PublicId)
            {
                ResourceType = ResourceType.Video
            };

            var result = await _cloudinary.DestroyAsync(deletionParams);

            _logger.LogInformation("Cloudinary delete result: {Result}", result.Result);

            if (result.Result == "not found")
            {
                _logger.LogWarning("Video with PublicId {PublicId} not found in Cloudinary, probably already deleted.", request.PublicId);
                return Unit.Value;
            }

            if (result.Result != "ok")
            {
                _logger.LogError("Failed to delete video from Cloudinary. Cloudinary returned: {Result}", result.Result);
                throw new Exception($"Failed to delete video. Cloudinary returned: {result.Result}");
            }

            _logger.LogInformation("Video with PublicId {PublicId} deleted successfully from Cloudinary.", request.PublicId);
            return Unit.Value;
        }
    }
}
