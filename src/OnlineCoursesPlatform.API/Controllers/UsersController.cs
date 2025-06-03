using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineCoursesPlatform.Application.Features.Users.Dto;
using OnlineCoursesPlatform.Application.Features.Users.Commands;
using OnlineCoursesPlatform.Domain.Entities;
using OnlineCoursesPlatform.Application.Features.Users.Queries;

namespace OnlineCoursesPlatform.API.Controllers;

[ApiController]
[Route("api")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("users")]
    public async Task<IActionResult> GetAll([FromQuery] int pageNumber =1, [FromQuery] int pageSize = 10)
    {
        var result = await _mediator.Send(new GetAllUsers(pageNumber,pageSize));
        return Ok(result);
    }

    [HttpGet]
    [Route("user/{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(new GetUserById(id));
        if (result == null) 
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    [Route ("user")]
    public async Task<IActionResult> Create([FromBody] CreateUserDto dto)
    {
        var result = await _mediator.Send(new CreateUser(dto.UserName, dto.Email));
        return CreatedAtAction(nameof(GetById), new {id = result.Id}, result);
    }

    [HttpPut]
    [Route("user/{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateUserDto dto)
    {
        var result = await _mediator.Send(new UpdateUser(id, dto.UserName, dto.Email));
        if (result == null)
            return NotFound();
        return Ok(result);
    }

    [HttpDelete]
    [Route ("user/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _mediator.Send(new DeleteUser(id));
        if (!success)
            return NotFound();

        return NoContent();
    }
}
