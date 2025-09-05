using MediatR;

namespace OnlineCoursesPlatform.Application.Features.Cloudinary.Commands
{
    public record DeleteCloudinaryImageCommand(string PublicId) : IRequest<Unit>;
}
