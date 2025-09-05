using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineCoursesPlatform.Application.Features.Cloudinary.Commands;
using OnlineCoursesPlatform.Application.Features.Cloudinary.Queries;

namespace OnlineCoursesPlatform.API.Controllers
{
    [ApiController]
    [Route("api/cloudinary")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] 
    public class CloudinaryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CloudinaryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Get Signature
        [Authorize(Roles = "Teacher,Admin")]
        [HttpGet("signature")]
        public async Task<IActionResult> GetUploadSignature([FromQuery] string? caption, [FromQuery] string? type)
        {
            var response = await _mediator.Send(new GetCloudinarySignatureQuery
            {
                Caption = caption,
                ResourceType = type ?? "video"
            });

            return Ok(response);
        }

        // Delete Video
        [Authorize(Roles = "Teacher,Admin")]
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteVideo([FromQuery] string publicId)
        {
            await _mediator.Send(new DeleteCloudinaryVideoCommand(publicId));
            return Ok(new { Message = "Video deleted successfully." });
        }

        // Delete Image
        [HttpDelete("delete-image")]
        public async Task<IActionResult> DeleteImage([FromQuery] string publicId)
        {
            await _mediator.Send(new DeleteCloudinaryImageCommand(publicId));
            return NoContent();
        }

        // Получение метаинформации (caption) по publicId
        //[Authorize(Roles = "Teacher,Admin")]
        //[HttpGet("meta/{publicId}")]
        //public async Task<IActionResult> GetVideoMeta(string publicId)
        //{
        //    var response = await _mediator.Send(new GetCloudinaryMetaQuery(publicId));
        //    return Ok(response);
        //}
    }
}
