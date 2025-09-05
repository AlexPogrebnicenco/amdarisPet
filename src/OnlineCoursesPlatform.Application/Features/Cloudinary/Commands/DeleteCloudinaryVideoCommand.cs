using MediatR;

namespace OnlineCoursesPlatform.Application.Features.Cloudinary.Commands
{
    public record DeleteCloudinaryVideoCommand(string PublicId) : IRequest<Unit>;

}
