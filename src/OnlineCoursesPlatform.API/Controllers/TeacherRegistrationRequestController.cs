using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineCoursesPlatform.Application.Features.TeacherRegistrationRequests.Commands;
using OnlineCoursesPlatform.Application.Features.TeacherRegistrationRequests.Dto;
using OnlineCoursesPlatform.Application.Features.TeacherRegistrationRequests.Queries;

[ApiController]
[Route("api/teacher-requests")]
public class TeacherRegistrationRequestController : ControllerBase
{
    private readonly IMediator _mediator;

    public TeacherRegistrationRequestController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // POST: api/teacher-requests
    [HttpPost]
    public async Task<IActionResult> CreateTeacherRegistrationRequest([FromBody] CreateTeacherRegistrationRequestDto dto)
    {
        await _mediator.Send(new CreateTeacherRegistrationRequestCommand(dto));
        return Ok(new { Message = "Teacher registration request submitted successfully. Please wait for approval." });
    }

    // GET: api/teacher-requests
    [HttpGet]
    public async Task<IActionResult> GetPendingRequests([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var requests = await _mediator.Send(new GetPendingTeacherRequestsQuery(page, pageSize));
        return Ok(requests);
    }

    // PUT: api/teacher-requests/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateTeacherRequestStatusDto dto)
    {
        await _mediator.Send(new UpdateTeacherRequestStatusCommand(id, dto.Status));
        return Ok(new { Message = $"Teacher request {dto.Status.ToLower()}." });
    }
}
