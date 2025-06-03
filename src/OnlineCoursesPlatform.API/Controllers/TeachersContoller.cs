using Microsoft.AspNetCore.Mvc;
using OnlineCoursesPlatform.Application.Features.Teachers.Dto;
using OnlineCoursesPlatform.Domain.Entities;

namespace OnlineCoursesPlatform.API.Controllers;

[ApiController]
[Route("api")]
public class TeachersController : ControllerBase
{
    private static readonly List<Teacher> teachers = new()
    {
        new Teacher { Id = 1, TeacherName = "Dr. Smith", Email = "smith@edu.com" },
        new Teacher { Id = 2, TeacherName = "Prof. Johnson", Email = "johnson@edu.com" }
    };

    [HttpGet]
    [Route ("teachers")]
    public IActionResult GetAll()
    {
        return Ok(teachers.Select(TeacherDto.FromTeacher));
    }

    [HttpGet]
    [Route("teacher/{id}")]
    public IActionResult GetById(int id)
    {
        var teacher = teachers.FirstOrDefault(t => t.Id == id);
        if (teacher == null) return NotFound();
        return Ok(TeacherDto.FromTeacher(teacher));
    }

    [HttpPost]
    [Route ("teacher")]
    public IActionResult Create([FromBody] CreateTeacherDto dto)
    {
        var teacher = new Teacher
        {
            Id = teachers.Max(t => t.Id) + 1,
            TeacherName = dto.TeacherName,
            Email = dto.Email
        };

        teachers.Add(teacher);
        return CreatedAtAction(nameof(GetById), new { id = teacher.Id }, TeacherDto.FromTeacher(teacher));
    }

    [HttpPut]
    [Route("teacher/{id}")]
    public IActionResult Update(int id, [FromBody] CreateTeacherDto dto)
    {
        var teacher = teachers.FirstOrDefault(t => t.Id == id);
        if (teacher == null) return NotFound();

        teacher.TeacherName = dto.TeacherName;
        teacher.Email = dto.Email;

        return Ok(TeacherDto.FromTeacher(teacher));
    }

    [HttpDelete]
    [Route("teacher/{id}")]
    public IActionResult Delete(int id)
    {
        var teacher = teachers.FirstOrDefault(t => t.Id == id);
        if (teacher == null) return NotFound();

        teachers.Remove(teacher);
        return NoContent();
    }

}