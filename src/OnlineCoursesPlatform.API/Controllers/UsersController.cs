using Microsoft.AspNetCore.Mvc;
using OnlineCoursesPlatform.Application.Features.Users.Dto;
using OnlineCoursesPlatform.Domain.Entities;

namespace OnlineCoursesPlatform.API.Controllers;

[ApiController]
[Route("api")]
public class UsersController : ControllerBase
{
    private static readonly List<User> users = new List<User>
    {
        new User { Id = 1, UserName = "Alice", Email = "alice@example.com" },
        new User { Id = 2, UserName = "Bob", Email = "bob@example.com" }
    };

    [HttpGet]
    [Route ("users")]
    public IActionResult GetAll()
    {
        var result = users.Select(UserDto.FromUser);
        return Ok(result);
    }

    [HttpGet]
    [Route("user/{id}")]
    public IActionResult GetById(int id)
    {
        var user = users.FirstOrDefault(u => u.Id == id);
        if (user == null) return NotFound();

        return Ok(UserDto.FromUser(user));
    }

    [HttpPost]
    [Route ("user")]
    public IActionResult Create([FromBody] CreateUserDto dto)
    {
        var newUser = new User
        {
            Id = users.Max(u => u.Id) + 1,
            UserName = dto.UserName,
            Email = dto.Email,
        };

        users.Add(newUser);

        return CreatedAtAction(nameof(GetById), new { id = newUser.Id }, UserDto.FromUser(newUser));
    }

    [HttpPut]
    [Route("user/{id}")]
    public IActionResult Update(int id, [FromBody] CreateUserDto dto)
    {
        var user = users.FirstOrDefault(u => u.Id == id);
        if (user == null) return NotFound();

        user.UserName = dto.UserName;
        user.Email = dto.Email;

        return Ok(UserDto.FromUser(user));
    }

    [HttpDelete]
    [Route("user/{id}")]
    public IActionResult Delete(int id)
    {
        var user = users.FirstOrDefault(u => u.Id == id);
        if (user == null) return NotFound();

        users.Remove(user);
        return NoContent();
    }
}
