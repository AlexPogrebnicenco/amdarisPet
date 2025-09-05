using MediatR;
using OnlineCoursesPlatform.Application.Features.Categories.Dto;

namespace OnlineCoursesPlatform.Application.Features.Categories.Queries
{
    public record GetAllCategoriesQuery() : IRequest<List<CategoryDto>>;
}
