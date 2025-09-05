using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineCoursesPlatform.Application.Features.Tags.Queries;

namespace OnlineCoursesPlatform.API.Controllers
{
    [ApiController]
    [Route("api/tags")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] 
    public class TagsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TagsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTags()
        {
            var tags = await _mediator.Send(new GetAllTagsQuery());
            return Ok(tags);
        }
    }
}
