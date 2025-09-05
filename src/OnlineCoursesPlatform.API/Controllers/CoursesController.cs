using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using OnlineCoursesPlatform.Application.Features.Courses.Commands;
using OnlineCoursesPlatform.Application.Features.Courses.Dto;
using OnlineCoursesPlatform.Application.Features.Courses.Queries;
using OnlineCoursesPlatform.Application.Features.Enrollments.Commands;
using OnlineCoursesPlatform.Application.Features.Enrollments.Queries;

namespace OnlineCoursesPlatform.API.Controllers
{
    [ApiController]
    [Route("api/courses")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] // Все методы доступны только авторизованным пользователям
    public class CoursesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CoursesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Create 
        [Authorize(Roles = "Teacher,Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateCourse([FromBody] CreateCourseDto dto)
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                return Unauthorized(new { Message = "User ID not found in token." });
            }

            var userId = int.Parse(userIdClaim.Value);

            var courseId = await _mediator.Send(new CreateCourseCommand(dto, userId));

            return Ok(new { Message = "Course created successfully", CourseId = courseId });
        }

        // Update 
        [Authorize(Roles = "Teacher,Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse(int id, [FromBody] UpdateCourseDto dto)
        {
            await _mediator.Send(new UpdateCourseCommand(id, dto));
            return NoContent();
        }

        // Delete 
        [Authorize(Roles = "Teacher,Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            await _mediator.Send(new DeleteCourseCommand(id));
            return NoContent();
        }

        // PATCH method
        [Authorize(Roles = "Teacher,Admin")]
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchCourse(int id, [FromBody] JsonPatchDocument<UpdateCourseDto> patchDoc)
        {
            if (patchDoc == null)
                return BadRequest();

            // Передаём патч-документ в команду (всю логику заберёт хендлер)
            await _mediator.Send(new PatchCourseCommand(id, patchDoc));

            return NoContent();
        }


        // Get by Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourseById(int id)
        {
            var course = await _mediator.Send(new GetCourseByIdQuery(id));
            return Ok(course);
        }

        // Get All
        [HttpGet]
        public async Task<IActionResult> GetCourses([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var courses = await _mediator.Send(new GetCoursesQuery(pageNumber, pageSize));
            return Ok(courses);
        }

        // Get Most Popular
        [HttpGet("most-popular")]
        public async Task<IActionResult> GetMostPopularCourses([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var courses = await _mediator.Send(new GetMostPopularCoursesQuery(pageNumber, pageSize));
            return Ok(courses);
        }

        // Get Last Created
        [HttpGet("last-created")]
        public async Task<IActionResult> GetLastCreatedCourses([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var courses = await _mediator.Send(new GetLastCreatedCoursesQuery(pageNumber, pageSize));
            return Ok(courses);
        }

        // Get Last Modified
        [HttpGet("last-modified")]
        public async Task<IActionResult> GetLastModifiedCourses([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var courses = await _mediator.Send(new GetLastModifiedCoursesQuery(pageNumber, pageSize));
            return Ok(courses);
        }

        // Get Longest
        [HttpGet("longest")]
        public async Task<IActionResult> GetLongestCourses([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var courses = await _mediator.Send(new GetLongestCoursesQuery(pageNumber, pageSize));
            return Ok(courses);
        }

        // Get Shortest
        [HttpGet("shortest")]
        public async Task<IActionResult> GetShortestCourses([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var courses = await _mediator.Send(new GetShortestCoursesQuery(pageNumber, pageSize));
            return Ok(courses);
        }

        // Get by Tags
        [HttpGet("by-tags")]
        public async Task<IActionResult> GetCoursesByTags([FromQuery] List<string> tags, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var courses = await _mediator.Send(new GetCoursesByTagsQuery(tags, pageNumber, pageSize));
            return Ok(courses);
        }

        // Get by Category
        [HttpGet("by-category/{categoryId}")]
        public async Task<IActionResult> GetCoursesByCategory(int categoryId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var courses = await _mediator.Send(new GetCoursesByCategoryQuery(categoryId, pageNumber, pageSize));
            return Ok(courses);
        }

        // Get Alphabetically Ascending
        [HttpGet("alphabetical-asc")]
        public async Task<IActionResult> GetCoursesAlphabeticallyAsc([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var courses = await _mediator.Send(new GetCoursesAlphabeticallyAscQuery(pageNumber, pageSize));
            return Ok(courses);
        }

        // Get Alphabetically Descending
        [HttpGet("alphabetical-desc")]
        public async Task<IActionResult> GetCoursesAlphabeticallyDesc([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var courses = await _mediator.Send(new GetCoursesAlphabeticallyDescQuery(pageNumber, pageSize));
            return Ok(courses);
        }

        // Search 
        [HttpGet("search")]
        public async Task<IActionResult> SearchCoursesByTitle([FromQuery] string query, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _mediator.Send(new GetCoursesByTitleQuery(query, pageNumber, pageSize));
            return Ok(result);
        }


        [Authorize(Roles = "Teacher,Admin")]
        [HttpGet("teacher/own-courses")]
        public async Task<IActionResult> GetTeacherOwnCourses(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? sort = null,
            [FromQuery] string? tag = null,
            [FromQuery] string? search = null)
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized();

            var userId = int.Parse(userIdClaim.Value);

            var result = await _mediator.Send(new GetCoursesByAuthorQuery(
                userId, pageNumber, pageSize, sort, tag, search));

            return Ok(result);
        }



        // Enrollment process
        [HttpPost("{id}/enroll")]
        public async Task<IActionResult> EnrollInCourse(int id)
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized();

            var userId = int.Parse(userIdClaim.Value);

            await _mediator.Send(new EnrollCourseCommand(userId, id));

            return Ok(new { Message = "Successfully enrolled in course." });
        }

        [HttpDelete("{id}/unenroll")]
        public async Task<IActionResult> UnenrollFromCourse(int id)
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized();

            var userId = int.Parse(userIdClaim.Value);

            //  проверять наличие записи 
            await _mediator.Send(new UnenrollCourseCommand(userId, id));

            return Ok(new { Message = "Successfully unenrolled from course." });
        }

        [HttpGet("{id}/is-enrolled")]
        public async Task<IActionResult> IsEnrolled(int id)
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized();

            var userId = int.Parse(userIdClaim.Value);

            var isEnrolled = await _mediator.Send(new IsUserEnrolledQuery(userId, id));

            return Ok(new { IsEnrolled = isEnrolled });
        }

        [HttpGet("{id}/enrollments-count")]
        public async Task<IActionResult> GetCourseEnrollmentCount(int id)
        {
            var count = await _mediator.Send(new GetCourseEnrollmentCountQuery(id));
            return Ok(new { EnrollmentCount = count });
        }
    }
}
