using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineCoursesPlatform.Application.Common.Dto;
using OnlineCoursesPlatform.Application.Features.Courses.Dto;
using OnlineCoursesPlatform.Application.Features.Enrollments.Queries;
using OnlineCoursesPlatform.Application.Features.Users.Commands;
using OnlineCoursesPlatform.Application.Features.Users.Dto;
using OnlineCoursesPlatform.Application.Features.Users.Queries;

[ApiController]
[Route("api/users")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator) => _mediator = mediator;

    [Authorize(Roles = "User,Teacher,Admin")]
    [HttpPut("account")]
    public async Task<IActionResult> UpdateAccount([FromBody] UpdateUserDto dto)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out var userId))
        {
            return Unauthorized("Invalid or missing user ID in token.");
        }

        var result = await _mediator.Send(new UpdateUser(userId, dto));
        return Ok(result);
    }

    [Authorize(Roles = "User,Teacher,Admin")]
    [HttpGet("me/enrollments")]
    public async Task<ActionResult<PagedResult<CourseDto>>> GetMyEnrolledCourses(
    [FromQuery] int pageNumber = 1,
    [FromQuery] int pageSize = 10,
    [FromQuery] string? sort = null,
    [FromQuery] string? tag = null,
    [FromQuery] string? search = null)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out var userId))
            return Unauthorized("Invalid or missing user ID in token.");

        var result = await _mediator.Send(new GetUserEnrolledCoursesQuery(
            userId, pageNumber, pageSize, sort, tag, search));

        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var result = await _mediator.Send(new GetAllUsers(pageNumber, pageSize));
        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(new GetUserById(id));
        return result == null ? NotFound() : Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserDto dto)
    {
        var result = await _mediator.Send(new CreateUser(dto));
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _mediator.Send(new DeleteUser(id));
        return success ? NoContent() : NotFound();
    }
}
