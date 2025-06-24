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

    [HttpPost]
    public async Task<IActionResult> CreateTeacherRegistrationRequest([FromBody] CreateTeacherRegistrationRequestDto dto)
    {
        await _mediator.Send(new CreateTeacherRegistrationRequestCommand(dto));
        return Ok(new { Message = "Teacher registration request submitted successfully. Please wait for approval." });
    }

    [HttpGet]
    public async Task<IActionResult> GetPendingRequests()
    {
        var requests = await _mediator.Send(new GetPendingTeacherRequestsQuery());
        return Ok(requests);
    }

    [HttpPost("{id}/approve")]
    public async Task<IActionResult> Approve(int id)
    {
        await _mediator.Send(new ApproveTeacherRequestCommand(id));
        return Ok(new { Message = "Teacher request approved." });
    }

    [HttpPost("{id}/reject")]
    public async Task<IActionResult> Reject(int id)
    {
        await _mediator.Send(new RejectTeacherRequestCommand(id));
        return Ok(new { Message = "Teacher request rejected." });
    }
}
