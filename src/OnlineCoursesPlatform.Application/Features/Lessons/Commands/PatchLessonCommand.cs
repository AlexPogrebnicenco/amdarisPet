using MediatR;
using OnlineCoursesPlatform.Application.Features.Lessons.Dto;
using Microsoft.AspNetCore.JsonPatch;

namespace OnlineCoursesPlatform.Application.Features.Lessons.Commands
{
    public record PatchLessonCommand(int CourseId, int OrderNumber, JsonPatchDocument<UpdateLessonDto> PatchDoc) : IRequest<Unit>;
}
