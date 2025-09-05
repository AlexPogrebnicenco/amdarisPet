using MediatR;
using OnlineCoursesPlatform.Application.Features.Tags.Dto;

namespace OnlineCoursesPlatform.Application.Features.Tags.Queries
{
    public record GetAllTagsQuery() : IRequest<List<TagDto>>;
}
