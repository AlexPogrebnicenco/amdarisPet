using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineCoursesPlatform.Application.Features.TeacherRegistrationRequests.Commands;
using OnlineCoursesPlatform.Application.Features.TeacherRegistrationRequests.Dto;
using OnlineCoursesPlatform.Application.Features.TeacherRegistrationRequests.Queries;
using OnlineCoursesPlatform.Domain.Enums;

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
    [AllowAnonymous]
    public async Task<IActionResult> CreateTeacherRegistrationRequest([FromBody] CreateTeacherRegistrationRequestDto dto)
    {
        await _mediator.Send(new CreateTeacherRegistrationRequestCommand(dto));
        return Ok(new { Message = "Teacher registration request submitted successfully. Please wait for approval." });
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> GetPendingRequests([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var requests = await _mediator.Send(new GetPendingTeacherRequestsQuery(page, pageSize));
        return Ok(requests);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateTeacherRequestStatusDto dto)
    {
        await _mediator.Send(new UpdateTeacherRequestStatusCommand(id, dto.Status));
        return Ok(new { Message = $"Teacher request {dto.Status.ToString().ToLower()}." });
    }
}
