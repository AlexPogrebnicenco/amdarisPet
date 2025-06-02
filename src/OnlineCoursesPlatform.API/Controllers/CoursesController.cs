using Microsoft.AspNetCore.Mvc;
using OnlineCoursesPlatform.Application.Features.Courses.Dto;
using OnlineCoursesPlatform.Domain.Entities;

namespace OnlineCoursesPlatform.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoursesController : ControllerBase
{
    private static readonly List<Course> courses = new()
    {
        new Course { Id = 1, Title = "Intro to C#", Description = "Learn C# basics", DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow },
        new Course { Id = 2, Title = "ASP.NET Core", Description = "Web development with ASP.NET", DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow }
    };

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(courses.Select(CourseDto.FromCourse));
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var course = courses.FirstOrDefault(c => c.Id == id);
        if (course == null) return NotFound();
        return Ok(CourseDto.FromCourse(course));
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateCourseDto dto)
    {
        var course = new Course
        {
            Id = courses.Max(c => c.Id) + 1,
            Title = dto.Title,
            Description = dto.Description,
            DateCreated = DateTime.UtcNow,
            DateModified = DateTime.UtcNow
        };

        courses.Add(course);
        return CreatedAtAction(nameof(GetById), new { id = course.Id }, CourseDto.FromCourse(course));
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] CreateCourseDto dto)
    {
        var course = courses.FirstOrDefault(c => c.Id == id);
        if (course == null) return NotFound();

        course.Title = dto.Title;
        course.Description = dto.Description;
        course.DateModified = DateTime.UtcNow;

        return Ok(CourseDto.FromCourse(course));
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var course = courses.FirstOrDefault(c => c.Id == id);
        if (course == null) return NotFound();

        courses.Remove(course);
        return NoContent();
    }

}