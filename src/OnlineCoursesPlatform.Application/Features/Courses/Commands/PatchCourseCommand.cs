using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using OnlineCoursesPlatform.Application.Features.Courses.Dto;

namespace OnlineCoursesPlatform.Application.Features.Courses.Commands
{
    public record PatchCourseCommand(int Id, JsonPatchDocument<UpdateCourseDto> PatchDoc) : IRequest<Unit>;
}
