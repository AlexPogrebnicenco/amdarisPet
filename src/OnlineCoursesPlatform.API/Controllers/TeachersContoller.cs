using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineCoursesPlatform.Application.Features.Teachers.Dto;
using OnlineCoursesPlatform.Application.Features.Teachers.Commands;
using OnlineCoursesPlatform.Application.Features.Teachers.Queries;

namespace OnlineCoursesPlatform.API.Controllers
{
    [ApiController]
    [Route("api")]
    public class TeachersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TeachersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("teachers")]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _mediator.Send(new GetAllTeachers(pageNumber, pageSize));
            return Ok(result);
        }

        [HttpGet]
        [Route("teacher/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetTeacherById(id));
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        [Route("teacher")]
        public async Task<IActionResult> Create([FromBody] CreateTeacherDto dto)
        {
            var result = await _mediator.Send(new CreateTeacher(dto));
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut]
        [Route("teacher/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateTeacherDto dto)
        {
            var result = await _mediator.Send(new UpdateTeacher(id, dto));
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpDelete]
        [Route("teacher/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _mediator.Send(new DeleteTeacher(id));
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
