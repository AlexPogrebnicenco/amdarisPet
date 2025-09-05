using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using OnlineCoursesPlatform.Application.Features.Lessons.Commands;
using OnlineCoursesPlatform.Application.Features.Lessons.Dto;
using OnlineCoursesPlatform.Application.Features.Lessons.Queries;

namespace OnlineCoursesPlatform.API.Controllers
{
    [ApiController]
    [Route("api/courses/{courseId}/lessons")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class LessonsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LessonsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Create lesson
        [Authorize(Roles = "Teacher,Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateLesson(int courseId, [FromBody] CreateLessonDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var lessonId = await _mediator.Send(new CreateLessonCommand(courseId, dto));
            return Ok(new { Message = "Lesson created successfully", LessonId = lessonId });
        }

        // Delete lesson by order number
        [Authorize(Roles = "Teacher,Admin")]
        [HttpDelete("{orderNumber}")]
        public async Task<IActionResult> DeleteLesson(int courseId, int orderNumber)
        {
            await _mediator.Send(new DeleteLessonCommand(courseId, orderNumber));
            return NoContent();
        }

        // Patch lesson by order number
        [Authorize(Roles = "Teacher,Admin")]
        [HttpPatch("{orderNumber}")]
        public async Task<IActionResult> PatchLesson(int courseId, int orderNumber, [FromBody] JsonPatchDocument<UpdateLessonDto> patchDoc)
        {
            if (patchDoc == null)
                return BadRequest("Patch document cannot be null.");

            await _mediator.Send(new PatchLessonCommand(courseId, orderNumber, patchDoc));
            return NoContent();
        }

        // Get lesson by order number
        [HttpGet("{orderNumber}")]
        public async Task<IActionResult> GetLessonByOrderNumber(int courseId, int orderNumber)
        {
            var lesson = await _mediator.Send(new GetLessonByOrderNumberQuery(courseId, orderNumber));
            return Ok(lesson);
        }

        [HttpGet]
        public async Task<IActionResult> GetLessonsByCourseId(int courseId)
        {
            var lessons = await _mediator.Send(new GetLessonsByCourseIdQuery(courseId));
            return Ok(lessons);
        }

    }
}
