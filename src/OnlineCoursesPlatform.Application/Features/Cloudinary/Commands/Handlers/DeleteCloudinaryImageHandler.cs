using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudinaryDotNet.Actions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace OnlineCoursesPlatform.Application.Features.Cloudinary.Commands.Handlers
{
    public class DeleteCloudinaryImageHandler : IRequestHandler<DeleteCloudinaryImageCommand, Unit>
    {
        private readonly CloudinaryDotNet.Cloudinary _cloudinary;
        private readonly ILogger<DeleteCloudinaryImageHandler> _logger;

        public DeleteCloudinaryImageHandler(CloudinaryDotNet.Cloudinary cloudinary, ILogger<DeleteCloudinaryImageHandler> logger)
        {
            _cloudinary = cloudinary;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteCloudinaryImageCommand request, CancellationToken cancellationToken)
        {
            var deletionParams = new DeletionParams(request.PublicId)
            {
                ResourceType = ResourceType.Image
            };

            var result = await _cloudinary.DestroyAsync(deletionParams);

            _logger.LogInformation("Cloudinary image delete result: {Result}", result.Result);

            if (result.Result == "not found")
            {
                _logger.LogWarning("Image with PublicId {PublicId} not found in Cloudinary.", request.PublicId);
                return Unit.Value;
            }

            if (result.Result != "ok")
            {
                _logger.LogError("Failed to delete image. Cloudinary returned: {Result}", result.Result);
                throw new Exception($"Failed to delete image. Cloudinary returned: {result.Result}");
            }

            _logger.LogInformation("Image with PublicId {PublicId} deleted successfully.", request.PublicId);
            return Unit.Value;
        }
    }
}
